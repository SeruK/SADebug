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
	//		An object to use as a context. If also a IDebugContext it will
	//		be sent to Dbg.DebugSystem.
	// (fmt, args)
	//		Format and arguments of the message if [Type] is Log, Warn or Error.
	// (exc)
	//		An exception if [Type] is exception or assertion.
	#region Log
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogIf( bool cond, string msg ) {
		if( cond ) { LogRelease( new DebugContext(), fmt: msg, args: null ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogIf( bool cond, string fmt, params object[] args ) {
		if( cond ) { LogRelease( new DebugContext(), fmt: fmt, args: args ); }
	}
	// --
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogIf( bool cond, UE.Object ctx, string msg ) {
		if( cond ) { LogRelease( new DebugContext( ctx ), fmt: msg, args: null ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogIf( bool cond, UE.Object ctx, string fmt, params object[] args ) {
		if( cond ) { LogRelease( new DebugContext( ctx ), fmt: fmt, args: args ); }
	}
	// --
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogIf( bool cond, DebugContext ctx, string msg ) {
		if( cond ) { LogRelease( ctx, fmt: msg, args: null ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogIf( bool cond, DebugContext ctx, string fmt, params object[] args ) {
		if( cond ) { LogRelease( ctx, fmt: fmt, args: args ); }
	}
	// --
	public static void LogReleaseIf( bool cond, string msg ) {
		if( cond ) { LogRelease( new DebugContext(), fmt: msg, args: null ); }
	}

	public static void LogReleaseIf( bool cond, string fmt, params object[] args ) {
		if( cond ) { LogRelease( new DebugContext(), fmt: fmt, args: args ); }
	}
	// --
	public static void LogReleaseIf( bool cond, UE.Object ctx, string msg ) {
		if( cond ) { LogRelease( ctx: new DebugContext( ctx ), fmt: msg, args: null ); }
	}

	public static void LogReleaseIf( bool cond, UE.Object ctx, string fmt, params object[] args ) {
		if( cond ) { LogRelease( ctx: new DebugContext( ctx ), fmt: fmt, args: args ); }
	}
	// --
	public static void LogReleaseIf( bool cond, DebugContext ctx, string msg ) {
		if( cond ) { LogRelease( ctx, fmt: msg, args: null ); }
	}

	public static void LogReleaseIf( bool cond, DebugContext ctx, string fmt, params object[] args ) {
		if( cond ) { LogRelease( ctx, fmt: fmt, args: args ); }
	}
	// --
	[Conditional( "DEBUG_LOGGING" )]
	public static void Log( string msg ) {
		LogRelease( new DebugContext(), fmt: msg, args: null );
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void Log( string fmt, params object[] args ) {
		LogRelease( new DebugContext(), fmt: fmt, args: args );
	}
	// --
	[Conditional( "DEBUG_LOGGING" )]
	public static void Log( UE.Object ctx, string msg ) {
		LogRelease( new DebugContext( ctx ), fmt: msg, args: null );
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void Log( UE.Object ctx, string fmt, params object[] args ) {
		LogRelease( new DebugContext( ctx ), fmt: fmt, args: args );
	}
	// --
	[Conditional( "DEBUG_LOGGING" )]
	public static void Log( DebugContext ctx, string msg ) {
		LogRelease( ctx, fmt: msg, args: null );
	}
	
	[Conditional( "DEBUG_LOGGING" )]
	public static void Log( DebugContext ctx, string fmt, params object[] args ) {
		LogRelease( ctx, fmt: fmt, args: args );
	}
	// --
	public static void LogRelease( string msg ) {
		LogRelease( new DebugContext(), fmt: msg, args: null );
	}

	public static void LogRelease( string fmt, params object[] args ) {
		LogRelease( new DebugContext(), fmt: fmt, args: args );
	}
	// --
	public static void LogRelease( UE.Object ctx, string msg ) {
		AddLogEntry( LogType.Log, new DebugContext( ctx ), msg, args: null );
	}
	
	public static void LogRelease( UE.Object ctx, string fmt, params object[] args ) {
		AddLogEntry( LogType.Log, new DebugContext( ctx ), fmt, args );
	}
	// --
	public static void LogRelease( DebugContext ctx, string msg ) {
		AddLogEntry( LogType.Log, ctx, msg, args: null );
	}
	
	public static void LogRelease( DebugContext ctx, string fmt, params object[] args ) {
		AddLogEntry( LogType.Log, ctx, fmt, args );
	}
	#endregion

	#region Warn
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarnIf( bool cond, string msg ) {
		if( cond ) { LogWarnRelease( new DebugContext(), fmt: msg, args: null ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarnIf( bool cond, string fmt, params object[] args ) {
		if( cond ) { LogWarnRelease( new DebugContext(), fmt: fmt, args: args ); }
	}
	// --
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarnIf( bool cond, UE.Object ctx, string msg ) {
		if( cond ) { LogWarnRelease( new DebugContext( ctx ), fmt: msg, args: null ); }
	}
	
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarnIf( bool cond, UE.Object ctx, string fmt, params object[] args ) {
		if( cond ) { LogWarnRelease( new DebugContext( ctx ), fmt: fmt, args: args ); }
	}
	// --
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarnIf( bool cond, DebugContext ctx, string msg ) {
		if( cond ) { LogWarnRelease( ctx, fmt: msg, args: null ); }
	}
	
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarnIf( bool cond, DebugContext ctx, string fmt, params object[] args ) {
		if( cond ) { LogWarnRelease( ctx, fmt: fmt, args: args ); }
	}
	// --
	public static void LogWarnReleaseIf( bool cond, string msg ) {
		if( cond ) { LogWarnRelease( new DebugContext(), fmt: msg, args: null ); }
	}
	
	public static void LogWarnReleaseIf( bool cond, string fmt, params object[] args ) {
		if( cond ) { LogWarnRelease( new DebugContext(), fmt: fmt, args: args ); }
	}
	// --
	public static void LogWarnReleaseIf( bool cond, UE.Object ctx, string msg ) {
		if( cond ) { LogWarnRelease( new DebugContext( ctx ), fmt: msg, args: null ); }
	}
	
	public static void LogWarnReleaseIf( bool cond, UE.Object ctx, string fmt, params object[] args ) {
		if( cond ) { LogWarnRelease( new DebugContext( ctx ), fmt: fmt, args: args ); }
	}
	// --
	public static void LogWarnReleaseIf( bool cond, DebugContext ctx, string msg ) {
		if( cond ) { LogWarnRelease( ctx, fmt: msg, args: null ); }
	}
	
	public static void LogWarnReleaseIf( bool cond, DebugContext ctx, string fmt, params object[] args ) {
		if( cond ) { LogWarnRelease( ctx, fmt: fmt, args: args ); }
	}
	// --
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarn( string msg ) {
		LogWarnRelease( new DebugContext(), fmt: msg, args: null );
	}
	
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarn( string fmt, params object[] args ) {
		LogWarnRelease( new DebugContext(), fmt: fmt, args: args );
	}
	// --
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarn( UE.Object ctx, string msg ) {
		LogWarnRelease( ctx: new DebugContext( ctx ), fmt: msg, args: null );
	}
	
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarn( UE.Object ctx, string fmt, params object[] args ) {
		LogWarnRelease( ctx: new DebugContext( ctx ), fmt: fmt, args: args );
	}
	// --
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarn( DebugContext ctx, string msg ) {
		LogWarnRelease( ctx, fmt: msg, args: null );
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogWarn( DebugContext ctx, string fmt, params object[] args ) {
		LogWarnRelease( ctx, fmt: fmt, args: args );
	}
	// --
	public static void LogWarnRelease( string msg ) {
		LogWarnRelease( new DebugContext(), fmt: msg, args: null );
	}
	
	public static void LogWarnRelease( string fmt, params object[] args ) {
		LogWarnRelease( new DebugContext(), fmt: fmt, args: args );
	}
	// --
	public static void LogWarnRelease( UE.Object ctx, string msg ) {
		AddLogEntry( LogType.Warning, new DebugContext( ctx ), msg, args: null );
	}
	
	public static void LogWarnRelease( UE.Object ctx, string fmt, params object[] args ) {
		AddLogEntry( LogType.Warning, new DebugContext( ctx ), fmt, args );
	}
	// --
	public static void LogWarnRelease( DebugContext ctx, string msg ) {
		AddLogEntry( LogType.Warning, ctx, msg, args: null );
	}

	public static void LogWarnRelease( DebugContext ctx, string fmt, params object[] args ) {
		AddLogEntry( LogType.Warning, ctx, fmt, args );
	}
	#endregion

	#region Error
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogErrorIf( bool cond, string msg ) {
		if( cond ) { LogErrorRelease( new DebugContext(), fmt: msg, args: null ); }
	}
	
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogErrorIf( bool cond, string fmt, params object[] args ) {
		if( cond ) { LogErrorRelease( new DebugContext(), fmt: fmt, args: args ); }
	}
	// --
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogErrorIf( bool cond, UE.Object ctx, string msg ) {
		if( cond ) { LogErrorRelease( new DebugContext( ctx ), fmt: msg, args: null ); }
	}
	
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogErrorIf( bool cond, UE.Object ctx, string fmt, params object[] args ) {
		if( cond ) { LogErrorRelease( new DebugContext( ctx ), fmt: fmt, args: args ); }
	}
	// --
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogErrorIf( bool cond, DebugContext ctx, string msg ) {
		if( cond ) { LogErrorRelease( ctx, fmt: msg, args: null ); }
	}
	
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogErrorIf( bool cond, DebugContext ctx, string fmt, params object[] args ) {
		if( cond ) { LogErrorRelease( ctx, fmt: fmt, args: args ); }
	}
	// --
	public static void LogErrorReleaseIf( bool cond, string msg ) {
		if( cond ) { LogErrorRelease( new DebugContext(), fmt: msg, args: null ); }
	}
	
	public static void LogErrorReleaseIf( bool cond, string fmt, params object[] args ) {
		if( cond ) { LogErrorRelease( new DebugContext(), fmt: fmt, args: args ); }
	}
	// --
	public static void LogErrorReleaseIf( bool cond, UE.Object ctx, string msg ) {
		if( cond ) { LogErrorRelease( new DebugContext( ctx ), fmt: msg, args: null ); }
	}
	
	public static void LogErrorReleaseIf( bool cond, UE.Object ctx, string fmt, params object[] args ) {
		if( cond ) { LogErrorRelease( new DebugContext( ctx ), fmt: fmt, args: args ); }
	}
	// --
	public static void LogErrorReleaseIf( bool cond, DebugContext ctx, string msg ) {
		if( cond ) { LogErrorRelease( ctx, fmt: msg, args: null ); }
	}
	
	public static void LogErrorReleaseIf( bool cond, DebugContext ctx, string fmt, params object[] args ) {
		if( cond ) { LogErrorRelease( ctx, fmt: fmt, args: args ); }
	}
	// --
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogError( string msg ) {
		LogErrorRelease( new DebugContext(), fmt: msg, args: null );
	}
	
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogError( string fmt, params object[] args ) {
		LogErrorRelease( new DebugContext(), fmt: fmt, args: args );
	}
	// --
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogError( UE.Object ctx, string msg ) {
		LogErrorRelease( new DebugContext( ctx ), fmt: msg, args: null );
	}
	
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogError( UE.Object ctx, string fmt, params object[] args ) {
		LogErrorRelease( new DebugContext( ctx ), fmt: fmt, args: args );
	}
	// --
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogError( DebugContext ctx, string msg ) {
		LogErrorRelease( ctx, fmt: msg, args: null );
	}
	
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogError( DebugContext ctx, string fmt, params object[] args ) {
		LogErrorRelease( ctx, fmt: fmt, args: args );
	}
	// --
	public static void LogErrorRelease( string msg ) {
		LogErrorRelease( new DebugContext(), fmt: msg, args: null );
	}
	
	public static void LogErrorRelease( string fmt, params object[] args ) {
		LogErrorRelease( new DebugContext(), fmt: fmt, args: args );
	}
	// --
	public static void LogErrorRelease( UE.Object ctx, string msg ) {
		AddLogEntry( LogType.Error, new DebugContext( ctx ), msg, args: null );
	}
	
	public static void LogErrorRelease( UE.Object ctx, string fmt, params object[] args ) {
		AddLogEntry( LogType.Error, new DebugContext( ctx ), fmt, args );
	}
	// --
	public static void LogErrorRelease( DebugContext ctx, string fmt ) {
		AddLogEntry( LogType.Error, ctx, fmt, args: null );
	}
	
	public static void LogErrorRelease( DebugContext ctx, string fmt, params object[] args ) {
		AddLogEntry( LogType.Error, ctx, fmt, args );
	}
	#endregion

	#region Exc
	[Conditional( "DEBUG_LOGGING" )]
	public static void LogExcIf( bool cond, Exception exc ) {
		if( cond ) { LogExcRelease( new DebugContext(), exc ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogExcIf( bool cond, UE.Object ctx, Exception exc ) {
		if( cond ) { LogExcRelease( new DebugContext( ctx ), exc ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogExcIf( bool cond, DebugContext ctx, Exception exc ) {
		if( cond ) { LogExcRelease( ctx, exc ); }
	}

	public static void LogExcReleaseIf( bool cond, Exception exc ) {
		if( cond ) { LogExcRelease( new DebugContext(), exc ); }
	}

	public static void LogExcReleaseIf( bool cond, UE.Object ctx, Exception exc ) {
		if( cond ) { LogExcRelease( new DebugContext( ctx ), exc ); }
	}

	public static void LogExcReleaseIf( bool cond, DebugContext ctx, Exception exc ) {
		if( cond ) { LogExcRelease( ctx, exc ); }
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogExc( Exception exc ) {
		LogExcRelease( new DebugContext(), exc );
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogExc( UE.Object ctx, Exception exc ) {
		LogExcRelease( new DebugContext( ctx ), exc );
	}

	[Conditional( "DEBUG_LOGGING" )]
	public static void LogExc( DebugContext ctx, Exception exc ) {
		LogExcRelease( ctx, exc );
	}

	public static void LogExcRelease( Exception exc ) {
		LogExcRelease( new DebugContext(), exc );
	}

	public static void LogExcRelease( UE.Object ctx, Exception exc ) {
		AddLogEntry( LogType.Exception, new DebugContext( ctx ), exc );
	}

	public static void LogExcRelease( DebugContext ctx, Exception exc ) {
		AddLogEntry( LogType.Exception, ctx, exc );
	}
	#endregion
}
#if COMPILE_NAMESPACE
}
#endif