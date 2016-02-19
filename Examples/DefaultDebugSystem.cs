using UnityEngine;
using UE = UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DefaultDebugSystem : IDebugSystem {
	public struct LogEntry {
		public readonly int Frame;
		public readonly Dbg.LogType LogType;
		public readonly string Message;

		public LogEntry( int frame, Dbg.LogType logType, Exception exc, string message ) {
			this.Frame = frame;
			this.LogType = logType;
			this.Message = message;
		}
	}

	public struct ObjectLog {
		public UE.Object Object {
			get { return (UE.Object)objectRef.Target; }
		}
		public IEnumerable<LogEntry> Logs {
			get { return logs; }
		}

		private readonly WeakReference objectRef;
		private readonly Queue<LogEntry> logs;
		private readonly int logLength;

		public ObjectLog( UE.Object obj, int logLength ) {
			this.objectRef = new WeakReference( obj );
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

	private Dictionary<int, ObjectLog> entries = new Dictionary<int, ObjectLog>();

	public DefaultDebugSystem() {}

	public ICollection<ObjectLog> Logs {
		get { return entries.Values; }
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

	public void AddLogEntry( Dbg.LogType logType, IDebugContext ctx, Exception exc, string message, out bool squelch ) {
		squelch = false;
		var obj = ctx as UE.Object;
		int instanceId = obj.GetInstanceID();
		if( !entries.ContainsKey( instanceId ) ) {
			entries[ instanceId ] = new ObjectLog( obj, logLength: 10 );
		}
		entries[ instanceId ].AddLogEntry( new LogEntry( Time.frameCount, logType, exc, message ) );
	}
}