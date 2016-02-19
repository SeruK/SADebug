using UnityEngine;
using UE = UnityEngine;
using System;
using System.Diagnostics;

#if COMPILE_NAMESPACE
using SA;

namespace SA {
#endif
public static partial class Dbg {
	// Follows the format Log[Type][Release][If]( [cond], [ctx], (format, args)/(exc) )
	// [Type]
	//		Should be obvious.
	// [Release]
	//		Messages should be logged in release (normally removed during compilation).
	// [If]
	//		Message will be logged if a condition [cond] is fulfilled.
	// [ctx]
	//		An UnityEngine.Object to use as a context. If also a IDebugContext it will
	//		be sent to Dbg.DebugSystem.
	// (format, args)
	//		Format and arguments of the message if [Type] is Log, Warn or Error.
	// (exc)
	//		An exception if [Type] is exception or assertion.
	#region Log
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogIf( bool cond, string format, params object[] args ) {
		if( cond ) { LogRelease( null, format, args ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogIf( bool cond, UE.Object ctx, string format, params object[] args ) {
		if( cond ) { LogRelease( ctx, format, args ); }
	}

	public static void LogReleaseIf( bool cond, string format, params object[] args ) {
		if( cond ) { LogRelease( null, format, args ); }
	}

	public static void LogReleaseIf( bool cond, UE.Object ctx, string format, params object[] args ) {
		if( cond ) { LogRelease( ctx, format, args ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void Log( string format, params object[] args ) {
		LogRelease( null, format, args );
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void Log( UE.Object ctx, string format, params object[] args ) {
		LogRelease( ctx, format, args );
	}

	public static void LogRelease( string format, params object[] args ) {
		LogRelease( null, format, args );
	}

	public static void LogRelease( UE.Object ctx, string format, params object[] args ) {
		AddLogEntry( LogType.Log, ctx, format, args );
	}
	#endregion

	#region Warn
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarnIf( bool cond, string format, params object[] args ) {
		if( cond ) { LogWarnRelease( null, format, args ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarnIf( bool cond, UE.Object ctx, string format, params object[] args ) {
		if( cond ) { LogWarnRelease( ctx, format, args ); }
	}

	public static void LogWarnReleaseIf( bool cond, string format, params object[] args ) {
		if( cond ) { LogWarnRelease( null, format, args ); }
	}

	public static void LogWarnReleaseIf( bool cond, UE.Object ctx, string format, params object[] args ) {
		if( cond ) { LogWarnRelease( ctx, format, args ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarn( string format, params object[] args ) {
		LogWarnRelease( null, format, args );
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarn( UE.Object ctx, string format, params object[] args ) {
		LogWarnRelease( ctx, format, args );
	}

	public static void LogWarnRelease( string format, params object[] args ) {
		LogWarnRelease( null, format, args );
	}

	public static void LogWarnRelease( UE.Object ctx, string format, params object[] args ) {
		AddLogEntry( LogType.Warning, ctx, format, args );
	}
	#endregion

	#region Error
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogErrorIf( bool cond, string format, params object[] args ) {
		if( cond ) { LogErrorRelease( null, format, args ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogErrorIf( bool cond, UE.Object ctx, string format, params object[] args ) {
		if( cond ) { LogErrorRelease( ctx, format, args ); }
	}

	public static void LogErrorReleaseIf( bool cond, string format, params object[] args ) {
		if( cond ) { LogErrorRelease( null, format, args ); }
	}

	public static void LogErrorReleaseIf( bool cond, UE.Object ctx, string format, params object[] args ) {
		if( cond ) { LogErrorRelease( ctx, format, args ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogError( string format, params object[] args ) {
		LogErrorRelease( null, format, args );
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogError( UE.Object ctx, string format, params object[] args ) {
		LogErrorRelease( ctx, format, args );
	}

	public static void LogErrorRelease( string format, params object[] args ) {
		LogErrorRelease( null, format, args );
	}

	public static void LogErrorRelease( UE.Object ctx, string format, params object[] args ) {
		AddLogEntry( LogType.Error, ctx, format, args );
	}
	#endregion

	#region Exc
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogExcIf( bool cond, Exception exc ) {
		if( cond ) { LogExcRelease( null, exc ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogExcIf( bool cond, UE.Object ctx, Exception exc ) {
		if( cond ) { LogExcRelease( ctx, exc ); }
	}

	public static void LogExcReleaseIf( bool cond, Exception exc ) {
		if( cond ) { LogExcRelease( null, exc ); }
	}

	public static void LogExcReleaseIf( bool cond, UE.Object ctx, Exception exc ) {
		if( cond ) { LogExcRelease( ctx, exc ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogExc( Exception exc ) {
		LogExcRelease( null, exc );
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogExc( UE.Object ctx, Exception exc ) {
		LogExcRelease( ctx, exc );
	}

	public static void LogExcRelease( Exception exc ) {
		LogExcRelease( null, exc );
	}

	public static void LogExcRelease( UE.Object ctx, Exception exc ) {
		AddLogEntry( LogType.Exception, ctx, exc );
	}
	#endregion
}
#if COMPILE_NAMESPACE
}
#endif