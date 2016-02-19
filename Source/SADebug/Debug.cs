using UnityEngine;
using UE = UnityEngine;
using System;
using System.Diagnostics;

#if COMPILE_NAMESPACE
using SA;

namespace SA {
#endif
public static partial class Dbg {
	#region Types
	public enum LogType {
		Log,
		Warning,
		Error,
		Exception,
		Assertion
	}
	#endregion

	#region Fields
	// Will receive log hooks with logged messages.
	public static IDebugSystem DebugSystem;
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

	#region Debug System Hook
	private static void AddLogEntry( LogType logType, UE.Object ctx, Exception exc ) {
		AddLogEntry( logType, ctx, exc, format: null, args: null );
	}

	private static void AddLogEntry( LogType logType, UE.Object ctx, string format, params object[] args ) {
		AddLogEntry( logType, ctx, exc: null, format: format, args: args );
	}

	private static void AddLogEntry( LogType logType, UE.Object ctx, Exception exc, string format, params object[] args ) {
		string message = format != null ? string.Format( format, args ) :
			exc != null ? exc.Message : null;

		bool squelch = false;

		if( DebugSystem != null && ctx is IDebugContext ) {
			DebugSystem.AddLogEntry( logType, (IDebugContext)ctx, exc, message, out squelch );
		}

		if( ctx is IDebugSquelcherContext ) {
			squelch |= ( (IDebugSquelcherContext)ctx ).ShouldSquelchLog( logType, exc, message );
		}

		if( squelch ) {
			return;
		}

		switch( logType ) {
			case LogType.Log: {
				UE.Debug.Log( message, ctx );
				break;
			}
			case LogType.Warning: {
				UE.Debug.LogWarning( message, ctx );
				break;
			}
			case LogType.Error: {
				UE.Debug.LogError( message, ctx );
				break;
			}
			case LogType.Exception:
			case LogType.Assertion: {
				UE.Debug.LogException( exc, ctx );
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