using UnityEngine;
using UE = UnityEngine;
using System;
using System.Diagnostics;

#if COMPILE_NAMESPACE
using SA;

namespace SA {
#endif
public static partial class Dbg {
	// Follows the format Log[Type][Release][If]( [cond], [ctx], (fmt, args)/(exc) )
	// [Type]
	//		Should be obvious.
	// [Release]
	//		Messages should be logged in release (normally removed during compilation).
	// [If]
	//		Message will be logged if a condition [cond] is fulfilled.
	// [ctx]
	//		An UnityEngine.Object to use as a context. If also a IDebugContext it will
	//		be sent to Dbg.DebugSystem.
	// (fmt, args)
	//		Format and arguments of the message if [Type] is Log, Warn or Error.
	// (exc)
	//		An exception if [Type] is exception or assertion.
	#region Log
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogIf( bool cond, string fmt, params object[] args ) {
		if( cond ) { LogRelease( ctx: null, fmt: fmt, args: args ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogIf( bool cond, object ctx, string fmt, params object[] args ) {
		if( cond ) { LogRelease( ctx: ctx, fmt: fmt, args: args ); }
	}

	public static void LogReleaseIf( bool cond, string fmt, params object[] args ) {
		if( cond ) { LogRelease( ctx: null, fmt: fmt, args: args ); }
	}

	public static void LogReleaseIf( bool cond, object ctx, string fmt, params object[] args ) {
		if( cond ) { LogRelease( ctx: ctx, fmt: fmt, args: args ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void Log( string fmt, params object[] args ) {
		LogRelease( ctx: null, fmt: fmt, args: args );
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void Log( object ctx, string fmt, params object[] args ) {
		LogRelease( ctx: ctx, fmt: fmt, args: args );
	}

	public static void LogRelease( string fmt, params object[] args ) {
		LogRelease( ctx: null, fmt: fmt, args: args );
	}

	public static void LogRelease( object ctx, string fmt, params object[] args ) {
		AddLogEntry( LogType.Log, ctx, fmt, args );
	}
	#endregion

	#region Warn
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarnIf( bool cond, string fmt, params object[] args ) {
		if( cond ) { LogWarnRelease( ctx: null, fmt: fmt, args: args ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarnIf( bool cond, object ctx, string fmt, params object[] args ) {
		if( cond ) { LogWarnRelease( ctx: ctx, fmt: fmt, args: args ); }
	}

	public static void LogWarnReleaseIf( bool cond, string fmt, params object[] args ) {
		if( cond ) { LogWarnRelease( ctx: null, fmt: fmt, args: args ); }
	}

	public static void LogWarnReleaseIf( bool cond, object ctx, string fmt, params object[] args ) {
		if( cond ) { LogWarnRelease( ctx: ctx, fmt: fmt, args: args ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarn( string fmt, params object[] args ) {
		LogWarnRelease( ctx: null, fmt: fmt, args: args );
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarn( object ctx, string fmt, params object[] args ) {
		LogWarnRelease( ctx: ctx, fmt: fmt, args: args );
	}

	public static void LogWarnRelease( string fmt, params object[] args ) {
		LogWarnRelease( ctx: null, fmt: fmt, args: args );
	}

	public static void LogWarnRelease( object ctx, string fmt, params object[] args ) {
		AddLogEntry( LogType.Warning, ctx, fmt, args );
	}
	#endregion

	#region Error
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogErrorIf( bool cond, string fmt, params object[] args ) {
		if( cond ) { LogErrorRelease( ctx: null, fmt: fmt, args: args ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogErrorIf( bool cond, object ctx, string fmt, params object[] args ) {
		if( cond ) { LogErrorRelease( ctx: ctx, fmt: fmt, args: args ); }
	}

	public static void LogErrorReleaseIf( bool cond, string fmt, params object[] args ) {
		if( cond ) { LogErrorRelease( ctx: null, fmt: fmt, args: args ); }
	}

	public static void LogErrorReleaseIf( bool cond, object ctx, string fmt, params object[] args ) {
		if( cond ) { LogErrorRelease( ctx: ctx, fmt: fmt, args: args ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogError( string fmt, params object[] args ) {
		LogErrorRelease( ctx: null, fmt: fmt, args: args );
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogError( object ctx, string fmt, params object[] args ) {
		LogErrorRelease( ctx: ctx, fmt: fmt, args: args );
	}

	public static void LogErrorRelease( string fmt, params object[] args ) {
		LogErrorRelease( ctx: null, fmt: fmt, args: args );
	}

	public static void LogErrorRelease( object ctx, string fmt, params object[] args ) {
		AddLogEntry( LogType.Error, ctx, fmt, args );
	}
	#endregion

	#region Exc
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogExcIf( bool cond, Exception exc ) {
		if( cond ) { LogExcRelease( null, exc ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogExcIf( bool cond, object ctx, Exception exc ) {
		if( cond ) { LogExcRelease( ctx, exc ); }
	}

	public static void LogExcReleaseIf( bool cond, Exception exc ) {
		if( cond ) { LogExcRelease( null, exc ); }
	}

	public static void LogExcReleaseIf( bool cond, object ctx, Exception exc ) {
		if( cond ) { LogExcRelease( ctx, exc ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogExc( Exception exc ) {
		LogExcRelease( null, exc );
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogExc( object ctx, Exception exc ) {
		LogExcRelease( ctx, exc );
	}

	public static void LogExcRelease( Exception exc ) {
		LogExcRelease( null, exc );
	}

	public static void LogExcRelease( object ctx, Exception exc ) {
		AddLogEntry( LogType.Exception, ctx, exc );
	}
	#endregion
}
#if COMPILE_NAMESPACE
}
#endif