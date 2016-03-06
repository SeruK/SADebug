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
		private readonly int logLength;

		public IEnumerable<LogEntry> Logs {
			get { return logs; }
		}

		public ObjectLog( UE.Object obj, int logLength ) {
			this.logs = new Queue<LogEntry>();
			this.logLength = logLength;
		}

		public void AddLogEntry( LogEntry entry ) {
			if( logs.Count >= logLength ) {
				logs.Dequeue();
			}
			logs.Enqueue( entry );
		}
	}

	public interface IMonitoredValue {
		void Record( object self );
	}

	public class MonitoredValue<TSelf, TVal> : IMonitoredValue {
		public readonly string name;
		private readonly List<TVal> recordedValues;
		private readonly Dbg.ValueRecorder<TSelf, TVal> recorder;
		private readonly Dbg.RecordPredicate<TSelf, TVal> predicate;

		public MonitoredValue( string name, Dbg.ValueRecorder<TSelf, TVal> recorder, Dbg.RecordPredicate<TSelf, TVal> predicate, int overFrames ) {

		}

		public void Record( object self ) {
			
		}
    }

	public struct ObjectMonitor {
		//public void Monitor<TSelf, TVal>( TSelf self, string name, Dbg.ValueRecorder<TSelf, TVal> recorder, Dbg.RecordPredicate<TSelf, TVal> predicate, int overFrames ) {
        private readonly Dictionary<string, List<IMonitoredValue>> values;
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
		public readonly ObjectMonitor monitor;

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
	public void AddLogEntry( LogType logType, object ctx, Exception exc, string message, out bool squelch ) {
		squelch = false;

		int hashCode = ctx.GetHashCode();
		if( !entries.ContainsKey( hashCode ) ) {
			entries[ hashCode ] = new ObjectLog( obj, logLength: 10 );
		}
		entries[ hashCode ].log.AddLogEntry( new LogEntry( Time.frameCount, logType, exc, message ) );
	}

	public void Monitor<TSelf, TVal>( TSelf self, string name, Dbg.ValueRecorder<TSelf, TVal> recorder, Dbg.RecordPredicate<TSelf, TVal> predicate, int overFrames ) {
		throw new NotImplementedException();
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