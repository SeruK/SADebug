using UnityEngine;
using UE = UnityEngine;
using System;
using System.Collections.Generic;

#if COMPILE_NAMESPACE
using SA;

namespace SA {
#endif
public static partial class Dbg {
	#region Types
	public interface IRecordedValue<TVal> {
		TVal recordedValue { get; }
	}

	public interface IRecordedValueDrawer<TVal> : IRecordedValue<TVal> {
		void Draw( string name, IList<RecordedValue<TVal>> frames );
	}

	public struct RecordedValue<TVal> {
		public readonly TVal value;
		public readonly int frame;

		public RecordedValue( TVal value, int frame ) {
			this.value = value;
			this.frame = frame;
		}
	}

	public delegate TVal RawValueRecorder<TSelf, TVal>( TSelf self );
	public delegate IRecordedValue<TVal> ValueRecorder<TSelf, TVal>( TSelf self );
	public delegate bool RecordPredicate<TSelf, TVal>( TSelf self, RecordedValue<TVal> last, RecordedValue<TVal> curr );

	internal struct RawValueWrapper<TVal> : IRecordedValue<TVal> {
		public TVal recordedValue { get; private set; }

		public RawValueWrapper( TVal value ) : this() {
			this.recordedValue = value;
		}
	}
	#endregion

	#region Monitoring
	public static void Monitor<TSelf, TVal>( TSelf self, string name, RawValueRecorder<TSelf, TVal> record, RecordPredicate<TSelf, TVal> ifTrue, int overFrames ) {
		Monitor( self, name, subSelf => new RawValueWrapper<TVal>( record( subSelf ) ), ifTrue, overFrames );
	}

	public static void Monitor<TSelf, TVal>( TSelf self, string name, ValueRecorder<TSelf, TVal> record, RecordPredicate<TSelf, TVal> ifTrue, int overFrames ) {
		if( Dbg.DebugSystem is IDebugMonitor ) {
			( (IDebugMonitor)Dbg.DebugSystem ).Monitor( self, name, record, ifTrue, overFrames );
		}
	}
	#endregion
}
#if COMPILE_NAMESPACE
}
#endif