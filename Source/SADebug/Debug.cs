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
	public static DebugContext Context( object ctx ) {
		return new DebugContext( ctx );
	}
	#endregion

	#region Debug System Hook
	private static void AddLogEntry( LogType logType, object ctx, Exception exc ) {
		AddLogEntry( logType, ctx, exc, fmt: null, args: null );
	}

	private static void AddLogEntry( LogType logType, object ctx, string fmt, params object[] args ) {
		AddLogEntry( logType, ctx, exc: null, fmt: fmt, args: args );
	}

	private static void AddLogEntry( LogType logType, object ctx, Exception exc, string fmt, params object[] args ) {
		if( ctx is DebugContext ) {
			ctx = ( (DebugContext)ctx ).obj;
		}
		
		string message = fmt != null ? string.Format( fmt, args ) :
			exc != null ? exc.Message : null;

		bool squelch = false;

		if( DebugSystem != null && ctx != null ) {
			DebugSystem.AddLogEntry( logType, ctx, exc, message, out squelch );
		}

		if( ctx is IDebugSquelcher ) {
			squelch |= ( (IDebugSquelcher)ctx ).ShouldSquelchLog( logType, exc, message );
		}

		if( squelch ) {
			return;
		}

		var ueCtx = ctx as UE.Object;

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