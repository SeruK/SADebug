using System;
using UnityEngine;
#if COMPILE_NAMESPACE
using SA;

namespace SA {
#endif
// A wrapper for contexts that are not UnityEngine objects.
public struct DebugContext {
	internal readonly object obj;

	internal DebugContext( object obj ) {
		this.obj = obj;
	}
}

// Can be used as a context for a DebugSystem and as well select
// whether to log output directed at it to the console or not.
public interface IDebugSquelcher {
	bool ShouldSquelchLog( LogType logType, Exception exc, string message );
}

public interface IDebugExcSquelcher : IDebugSquelcher {
	bool ShouldSquelchException( Exception exc );
}
#if COMPILE_NAMESPACE
}
#endif