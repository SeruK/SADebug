using UnityEngine;
using UE = UnityEngine;
using System;
using System.Diagnostics;

#if COMPILE_NAMESPACE
using SA;

namespace SA {
#endif
public static partial class Dbg {
	#region Fields
	// Will receive log hooks with logged messages.
	public static IDebugSystem DebugSystem;

	private static Message TempMessage = new Message();
	#endregion

	#region Break
	[Conditional( "DEBUG" )]
	public static void Break() {
		UE.Debug.Break();
	}

	[Conditional( "DEBUG" )]
	public static void BreakIf( bool cond ) {
		if( cond ) { UE.Debug.Break(); }
	}
	#endregion

	#region Boxing
	public class Message {
		public string stringValue {
			get {
				if( str == null ) {
					str = args == null ? fmt : string.Format(fmt, args);
				}
				return str;
			}
		}

		public static implicit operator string( Message msg ) {
			return msg.stringValue;
		}

		internal string str;
		internal string fmt;
		internal object[] args;

		internal void Reset( string fmt, object[] args ) {
			this.fmt = fmt;
			this.args = args;
			this.str = null;
		}
	}

	public static DebugContext Context( object ctx, UE.Object ueCtx = null ) {
		return new DebugContext( ctx, ueCtx );
	}
	#endregion

	#region Debug System Hook
	private static void AddLogEntry( LogType logType, DebugContext ctx, Exception exc ) {
		AddLogEntry( logType, ctx, exc, fmt: null, args: null );
	}

	private static void AddLogEntry( LogType logType, DebugContext ctx, string fmt, params object[] args ) {
		AddLogEntry( logType, ctx, exc: null, fmt: fmt, args: args );
	}

	private static void AddLogEntry( LogType logType, DebugContext ctx, Exception exc, string fmt, params object[] args ) {
		TempMessage.Reset( fmt, args );
		
		bool squelch = false;

		if( DebugSystem != null ) {
			DebugSystem.AddLogEntry( logType, ctx, exc, TempMessage, out squelch );
		}

		if( ctx.obj is IDebugSquelcher ) {
			squelch |= ( (IDebugSquelcher)ctx.obj ).ShouldSquelchLog( logType, exc, TempMessage );
		}

		if( squelch ) {
			return;
		}

		string message = TempMessage.stringValue;
		var ueCtx = ctx.unityObj;

		switch( logType ) {
			case LogType.Log: {
				UE.Debug.Log( message, ueCtx );
				break;
			}
			case LogType.Warning: {
				UE.Debug.LogWarning( message, ueCtx );
				break;
			}
			case LogType.Error: {
				UE.Debug.LogError( message, ueCtx );
				break;
			}
			case LogType.Exception:
			case LogType.Assert: {
				UE.Debug.LogException( exc, ueCtx );
				break;
			}
			default: {
				throw new Exception( "I should never get here" );
			}
		}
	}
	#endregion
}
#if COMPILE_NAMESPACE
}
#endif