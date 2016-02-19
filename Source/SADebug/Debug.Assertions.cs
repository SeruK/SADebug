using UnityEngine;
using UE = UnityEngine;
using UEAssert = UnityEngine.Assertions.Assert;
using System;
using System.Diagnostics;
using System.Collections.Generic;

#if COMPILE_NAMESPACE
using SA;

namespace SA {
#endif
public static partial class Dbg  {
	// Uses UnityEngine.Assertions.Assert under the hood, but adds DebugSystem hook.
	// So many overloads...
	public static class Assert {
		#region Properties
		public static bool RaiseExceptions {
			get { return UEAssert.raiseExceptions; }
		}
		#endregion

		#region IsTrue
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsTrue( bool cond ) {
			IsTrue( cond, ctx: null, format: null, args: null );
		}
		
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsTrue( bool cond, string format, params object[] args ) {
			IsTrue( cond, null, format, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsTrue( bool cond, UE.Object ctx, string format, params object[] args ) {
			try {
				string message = string.IsNullOrEmpty( format ) ? null : string.Format( format, args );
				UEAssert.IsTrue( cond, message );
			} catch( Exception exc ) {
				Throw( ctx, exc );
			}
		}
		#endregion

		#region IsFalse
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsFalse( bool cond ) {
			IsFalse( cond, ctx: null, format:null, args: null );
		}
		
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsFalse( bool cond, string format, params object[] args ) {
			IsFalse( cond, null, format, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsFalse( bool cond, UE.Object ctx, string format, params object[] args ) {
			try {
				string message = string.IsNullOrEmpty( format ) ? null : string.Format( format, args );
				UEAssert.IsFalse( cond, message );
			} catch( Exception exc ) {
				Throw( ctx, exc );
			}
		}
		#endregion

		#region IsNull
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsNull<T>( T value ) where T : class {
			IsNull( value, ctx: null, format: null, args: null );
		}
		
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsNull<T>( T value, string format, params object[] args ) where T : class {
			IsNull( value, null, format, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsNull<T>( T value, UE.Object ctx, string format, params object[] args ) where T : class {
			try {
				string message = string.IsNullOrEmpty( format ) ? null : string.Format( format, args );
				UEAssert.IsNull( value, message );
			} catch( Exception exc ) {
				Throw( ctx, exc );
			}
		}
		#endregion

		#region IsNotNull
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsNotNull<T>( T value ) where T : class {
			IsNotNull( value, ctx: null, format: null, args: null );
		}
		
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsNotNull<T>( T value, string format, params object[] args ) where T : class {
			IsNotNull( value, null, format, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsNotNull<T>( T value, UE.Object ctx, string format, params object[] args ) where T : class {
			try {
				string message = string.IsNullOrEmpty( format ) ? null : string.Format( format, args );
				UEAssert.IsNotNull( value, message );
			} catch( Exception exc ) {
				Throw( ctx, exc );
			}
		}
		#endregion

		#region AreEqual
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreEqual<T>( T expected, T actual ) {
			AreEqual( expected, actual, EqualityComparer<T>.Default, ctx: null, format: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreEqual<T>( T expected, T actual, UE.Object ctx ) {
			AreEqual( expected, actual, EqualityComparer<T>.Default, ctx, format: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreEqual<T>( T expected, T actual, string format, params object[] args ) {
			AreEqual( expected, actual, EqualityComparer<T>.Default, null, format, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreEqual<T>( T expected, T actual, UE.Object ctx, string format, params object[] args ) {
			AreEqual( expected, actual, EqualityComparer<T>.Default, ctx, format, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreEqual<T>( T expected, T actual, IEqualityComparer<T> comparer ) {
			AreEqual( expected, actual, comparer, ctx: null, format: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreEqual<T>( T expected, T actual, IEqualityComparer<T> comparer, UE.Object ctx ) {
			AreEqual( expected, actual, comparer, ctx, format: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreEqual<T>( T expected, T actual, IEqualityComparer<T> comparer, string format, params object[] args ) {
			AreEqual( expected, actual, comparer, ctx: null, format: format, args: args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreEqual<T>( T expected, T actual, IEqualityComparer<T> comparer, UE.Object ctx, string format, params object[] args ) {
			try {
				string message = string.IsNullOrEmpty( format ) ? null : string.Format( format, args );
				UEAssert.AreEqual( expected, actual, message, comparer );
			} catch( Exception exc ) {
				Throw( ctx, exc );
			}
		}
		#endregion

		#region AreNotEqual
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotEqual<T>( T expected, T actual ) where T : class {
			AreNotEqual( expected, actual, EqualityComparer<T>.Default, ctx: null, format: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotEqual<T>( T expected, T actual, UE.Object ctx ) where T : class {
			AreNotEqual( expected, actual, EqualityComparer<T>.Default, ctx, format: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotEqual<T>( T expected, T actual, string format, params object[] args ) {
			AreNotEqual( expected, actual, EqualityComparer<T>.Default, null, format, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotEqual<T>( T expected, T actual, UE.Object ctx, string format, params object[] args ) {
			AreNotEqual( expected, actual, EqualityComparer<T>.Default, ctx, format, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotEqual<T>( T expected, T actual, IEqualityComparer<T> comparer ) {
			AreNotEqual( expected, actual, comparer, ctx: null, format: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotEqual<T>( T expected, T actual, IEqualityComparer<T> comparer, UE.Object ctx ) {
			AreNotEqual( expected, actual, comparer, ctx, format: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotEqual<T>( T expected, T actual, IEqualityComparer<T> comparer, string format, params object[] args ) {
			AreNotEqual( expected, actual, comparer, ctx: null, format: format, args: args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotEqual<T>( T expected, T actual, IEqualityComparer<T> comparer, UE.Object ctx, string format, params object[] args ) {
			try {
				string message = string.IsNullOrEmpty( format ) ? null : string.Format( format, args );
				UEAssert.AreNotEqual( expected, actual, message, comparer );
			} catch( Exception exc ) {
				Throw( ctx, exc );
			}
		}
		#endregion

		#region AreApproxEqual
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreApproxEqual( float expected, float actual ) {
			AreApproxEqual( expected, actual, 0.00001f, null, format: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreApproxEqual( float expected, float actual, UE.Object ctx ) {
			AreApproxEqual( expected, actual, 0.00001f, ctx, format: null, args: null );
		}
		
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreApproxEqual( float expected, float actual, string format, params object[] args ) {
			AreApproxEqual( expected, actual, 0.00001f, null, format, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreApproxEqual( float expected, float actual, UE.Object ctx, string format, params object[] args ) {
			AreApproxEqual( expected, actual, 0.00001f, ctx, format, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreApproxEqual( float expected, float actual, float tolerance, string format, params object[] args ) {
			AreApproxEqual( expected, actual, tolerance, null, format, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreApproxEqual( float expected, float actual, float tolerance, UE.Object ctx, string format, params object[] args ) {
			try {
				string message = string.IsNullOrEmpty( format ) ? null : string.Format( format, args );
				UEAssert.AreApproximatelyEqual( expected, actual, tolerance, message );
			} catch( Exception exc ) {
				Throw( ctx, exc );
			}
		}
		#endregion

		#region AreNotApproxEqual
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotApproxEqual( float expected, float actual ) {
			AreNotApproxEqual( expected, actual, 0.00001f, null, format: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotApproxEqual( float expected, float actual, UE.Object ctx ) {
			AreNotApproxEqual( expected, actual, 0.00001f, ctx, format: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotApproxEqual( float expected, float actual, string format, params object[] args ) {
			AreNotApproxEqual( expected, actual, 0.00001f, null, format, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotApproxEqual( float expected, float actual, UE.Object ctx, string format, params object[] args ) {
			AreNotApproxEqual( expected, actual, 0.00001f, ctx, format, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotApproxEqual( float expected, float actual, float tolerance, string format, params object[] args ) {
			AreNotApproxEqual( expected, actual, tolerance, null, format, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotApproxEqual( float expected, float actual, float tolerance, UE.Object ctx, string format, params object[] args ) {
			try {
				string message = string.IsNullOrEmpty( format ) ? null : string.Format( format, args );
				UEAssert.AreNotApproximatelyEqual( expected, actual, tolerance, message );
			} catch( Exception exc ) {
				Throw( ctx, exc );
			}
		}
		#endregion

		#region Throwing
		private static void Throw( UE.Object ctx, Exception exc ) {
			AddLogEntry( LogType.Assertion, ctx, exc );
			throw exc; // Rethrow
		}
		#endregion
	}
}
#if COMPILE_NAMESPACE
}
#endif