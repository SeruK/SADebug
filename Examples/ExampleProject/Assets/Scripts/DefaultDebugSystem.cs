using UnityEngine;
using UE = UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// Tracks UnityEngine.Object contexts and provides a per-object log.
public class DefaultDebugSystem : IDebugSystem, IDebugMonitor {
	#region Types
	public struct LogEntry {
		public readonly int Frame;
		public readonly LogType LogType;
		public readonly string Message;

		public LogEntry( int frame, LogType logType, Exception exc, string message ) {
			this.Frame = frame;
			this.LogType = logType;
			this.Message = message;
		}
	}

	public struct ObjectLog {
		private readonly Queue<LogEntry> logs;
		private readonly int maxLogLength;

		public IEnumerable<LogEntry> Logs {
			get { return logs; }
		}

		public ObjectLog( int maxLogLength ) {
			this.logs = new Queue<LogEntry>();
			this.maxLogLength = maxLogLength;
		}

		public void AddLogEntry( LogEntry entry ) {
			if( logs.Count >= maxLogLength ) {
				logs.Dequeue();
			}
			logs.Enqueue( entry );
		}
	}

	public interface IMonitoredValue {
		void Record( object self, int frameCount );
	}

	public class MonitoredValue<TSelf, TVal> : IMonitoredValue {
		public readonly string name;
		private readonly List<Dbg.RecordedValue<TVal>> recordedValues;
		private readonly int maxRecordedValues;
		private readonly Dbg.ValueRecorder<TSelf, TVal> recorder;
		private readonly Dbg.RecordPredicate<TSelf, TVal> predicate;

		public MonitoredValue( string name, Dbg.ValueRecorder<TSelf, TVal> recorder, Dbg.RecordPredicate<TSelf, TVal> predicate, int maxRecordedValues ) {
			this.name = name;
			this.recorder = recorder;
			this.predicate = predicate;
			this.recordedValues = new List<Dbg.RecordedValue<TVal>>();
			this.maxRecordedValues = maxRecordedValues;
		}

		public void Record( object self, int frameCount ) {
			TVal newValue = recorder( (TSelf)self ).recordedValue;
			var newFrame = new Dbg.RecordedValue<TVal>( newValue, frameCount );
			bool shouldStore = true;
			if( predicate != null ) {
				Dbg.RecordedValue<TVal> lastFrame = recordedValues.Count == 0 ? newFrame :
					(Dbg.RecordedValue<TVal>) recordedValues[ recordedValues.Count - 1 ];
				shouldStore = predicate( (TSelf)self, lastFrame, newFrame );
			}
			if( shouldStore ) {
				if( recordedValues.Count + 1 > maxRecordedValues ) {
					recordedValues.RemoveAt( 0 );
				}
				recordedValues.Add( newFrame );
			}
		}
    }

	public class ObjectMonitor {
		//public void Monitor<TSelf, TVal>( TSelf self, string name, Dbg.ValueRecorder<TSelf, TVal> recorder, Dbg.RecordPredicate<TSelf, TVal> predicate, int overFrames ) {
        private Dictionary<string, List<IMonitoredValue>> values = new Dictionary<string, List<IMonitoredValue>>();

		public void AddMonitoredValue<TSelf, TVal>( string name, Dbg.ValueRecorder<TSelf, TVal> recorder, Dbg.RecordPredicate<TSelf, TVal> predicate, int maxRecordedValues ) {
			Dbg.Assert.IsFalse( string.IsNullOrEmpty( name ) );
			Dbg.Assert.IsFalse( values.ContainsKey( name ) );
			var monitoredValue = new MonitoredValue<TSelf, TVal>( name, recorder, predicate, maxRecordedValues );
			if( !values.ContainsKey( name ) ) {
				values[ name ] = new List<IMonitoredValue>();
			}
			values[ name ].Add( monitoredValue );
		}
	}

	public class DebugEntry {
		public bool ObjectAlive {
			get { return objectRef.IsAlive; }
		}
		public object Object {
			get { return objectRef.Target; }
		}

		private readonly WeakReference objectRef;
		public readonly ObjectLog log = new ObjectLog();
		public readonly ObjectMonitor monitor = new ObjectMonitor();

		public DebugEntry( object obj ) {
			this.objectRef = new WeakReference( obj );
		}
	}
	#endregion

	#region Fields
	private Dictionary<int, DebugEntry> entries;
	#endregion

	#region Properties
	public ICollection<DebugEntry> Entries {
		get { return entries.Values; }
	}
	#endregion

	#region Constructors
	public DefaultDebugSystem() {
		entries = new Dictionary<int, DebugEntry>();
	}
	#endregion

	#region Methods
	private DebugEntry GetOrAddDebugEntry( object ctx ) {
		Dbg.Assert.IsNotNull( ctx );
		int hashCode = ctx.GetHashCode();
		if( !entries.ContainsKey( hashCode ) ) {
			entries[ hashCode ] = new DebugEntry( ctx );
		}
		return entries[ hashCode ];
	}

	public void AddLogEntry( LogType logType, object ctx, Exception exc, string message, out bool squelch ) {
		squelch = false;

		DebugEntry entry = GetOrAddDebugEntry( ctx );
		entry.log.AddLogEntry( new LogEntry( Time.frameCount, logType, exc, message ) );
	}

	public void Monitor<TSelf, TVal>( TSelf self, string name, Dbg.ValueRecorder<TSelf, TVal> recorder, Dbg.RecordPredicate<TSelf, TVal> predicate, int maxRecordedFrames ) {
		DebugEntry entry = GetOrAddDebugEntry( self );
		entry.monitor.AddMonitoredValue( name, recorder, predicate, maxRecordedFrames );
	}

	public void Clean() {
		// TODO: This nicer (IndexBuffer)
		var keys = entries.Keys;
		
		bool anyDirty = false;
		foreach( int key in keys ) {
			if( entries[ key ].Object == null ) {
				anyDirty = true;
				break;
			}
		}

		if( anyDirty ) {
			List<int> copiedKeys = new List<int>( entries.Keys );
			for( int keyIndex = 0; keyIndex < copiedKeys.Count; ++keyIndex ) {
				int key = copiedKeys[ keyIndex ];
				if( entries[ key ].Object == null ) {
					entries.Remove( key );
				}
			}
		}
	}
	#endregion
}