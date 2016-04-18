using System;
using UnityEngine;
using UE = UnityEngine;
#if COMPILE_NAMESPACE
using SA;

namespace SA {
#endif
// A wrapper for contexts that are not UnityEngine objects.
public struct DebugContext {
	public readonly object obj;
	public readonly UE.Object unityObj;

	internal DebugContext( object obj ) : this( obj, null ) {}

	internal DebugContext( UE.Object unityObj ) : this( unityObj, unityObj ) {}
	
	internal DebugContext( object obj, UE.Object unityObj ) {
		this.obj = obj;
		this.unityObj = unityObj;
	}
}

// Can be used as a context for a DebugSystem and as well select
// whether to log output directed at it to the console or not.
public interface IDebugSquelcher {
	bool ShouldSquelchLog( LogType logType, Exception exc, Dbg.Message message );
}

public interface IDebugExcSquelcher : IDebugSquelcher {
	bool ShouldSquelchException( Exception exc );
}
#if COMPILE_NAMESPACE
}
#endif