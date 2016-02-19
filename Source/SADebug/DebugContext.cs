using System;
#if COMPILE_NAMESPACE
using SA;

namespace SA {
#endif
// If an object implements this, it can be used as a context
// for a DebugSystem.
public interface IDebugContext {}

// Can be used as a context for a DebugSystem and as well select
// whether to log output directed at it to the console or not.
public interface IDebugSquelcherContext : IDebugContext {
	bool ShouldSquelchLog( Dbg.LogType logType, Exception exc, string message );
}
#if COMPILE_NAMESPACE
}
#endif