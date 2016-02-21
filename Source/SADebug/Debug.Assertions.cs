﻿using UnityEngine;
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
			IsTrue( cond, ctx: null, fmt: null, args: null );
		}
		
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsTrue( bool cond, string fmt, params object[] args ) {
			IsTrue( cond, ctx: null, fmt: fmt, args: args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsTrue( bool cond, object ctx, string fmt, params object[] args ) {
			try {
				string message = string.IsNullOrEmpty( fmt ) ? null : string.Format( fmt, args );
				UEAssert.IsTrue( cond, message );
			} catch( Exception exc ) {
				Throw( ctx, exc );
			}
		}
		#endregion

		#region IsFalse
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsFalse( bool cond ) {
			IsFalse( cond, ctx: null, fmt:null, args: null );
		}
		
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsFalse( bool cond, string fmt, params object[] args ) {
			IsFalse( cond, ctx: null, fmt: fmt, args: args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsFalse( bool cond, object ctx, string fmt, params object[] args ) {
			try {
				string message = string.IsNullOrEmpty( fmt ) ? null : string.Format( fmt, args );
				UEAssert.IsFalse( cond, message );
			} catch( Exception exc ) {
				Throw( ctx, exc );
			}
		}
		#endregion

		#region IsNull
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsNull<T>( T value ) where T : class {
			IsNull( value, ctx: null, fmt: null, args: null );
		}
		
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsNull<T>( T value, string fmt, params object[] args ) where T : class {
			IsNull( value, ctx: null, fmt: fmt, args: args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsNull<T>( T value, object ctx, string fmt, params object[] args ) where T : class {
			try {
				string message = string.IsNullOrEmpty( fmt ) ? null : string.Format( fmt, args );
				UEAssert.IsNull( value, message );
			} catch( Exception exc ) {
				Throw( ctx, exc );
			}
		}
		#endregion

		#region IsNotNull
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsNotNull<T>( T value ) where T : class {
			IsNotNull( value, ctx: null, fmt: null, args: null );
		}
		
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsNotNull<T>( T value, string fmt, params object[] args ) where T : class {
			IsNotNull( value, ctx: null, fmt: fmt, args: args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void IsNotNull<T>( T value, object ctx, string fmt, params object[] args ) where T : class {
			try {
				string message = string.IsNullOrEmpty( fmt ) ? null : string.Format( fmt, args );
				UEAssert.IsNotNull( value, message );
			} catch( Exception exc ) {
				Throw( ctx, exc );
			}
		}
		#endregion

		#region AreEqual
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreEqual<T>( T expected, T actual ) {
			AreEqual( expected, actual, EqualityComparer<T>.Default, ctx: null, fmt: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreEqual<T>( T expected, T actual, object ctx ) {
			AreEqual( expected, actual, EqualityComparer<T>.Default, ctx, fmt: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreEqual<T>( T expected, T actual, string fmt, params object[] args ) {
			AreEqual( expected, actual, EqualityComparer<T>.Default, ctx: null, fmt: fmt, args: args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreEqual<T>( T expected, T actual, object ctx, string fmt, params object[] args ) {
			AreEqual( expected, actual, EqualityComparer<T>.Default, ctx, fmt, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreEqual<T>( T expected, T actual, IEqualityComparer<T> comparer ) {
			AreEqual( expected, actual, comparer, ctx: null, fmt: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreEqual<T>( T expected, T actual, IEqualityComparer<T> comparer, object ctx ) {
			AreEqual( expected, actual, comparer, ctx, fmt: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreEqual<T>( T expected, T actual, IEqualityComparer<T> comparer, string fmt, params object[] args ) {
			AreEqual( expected, actual, comparer, ctx: null, fmt: fmt, args: args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreEqual<T>( T expected, T actual, IEqualityComparer<T> comparer, object ctx, string fmt, params object[] args ) {
			try {
				string message = string.IsNullOrEmpty( fmt ) ? null : string.Format( fmt, args );
				UEAssert.AreEqual( expected, actual, message, comparer );
			} catch( Exception exc ) {
				Throw( ctx, exc );
			}
		}
		#endregion

		#region AreNotEqual
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotEqual<T>( T expected, T actual ) where T : class {
			AreNotEqual( expected, actual, EqualityComparer<T>.Default, ctx: null, fmt: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotEqual<T>( T expected, T actual, object ctx ) where T : class {
			AreNotEqual( expected, actual, EqualityComparer<T>.Default, ctx, fmt: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotEqual<T>( T expected, T actual, string fmt, params object[] args ) {
			AreNotEqual( expected, actual, EqualityComparer<T>.Default, ctx: null, fmt: fmt, args: args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotEqual<T>( T expected, T actual, object ctx, string fmt, params object[] args ) {
			AreNotEqual( expected, actual, EqualityComparer<T>.Default, ctx, fmt, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotEqual<T>( T expected, T actual, IEqualityComparer<T> comparer ) {
			AreNotEqual( expected, actual, comparer, ctx: null, fmt: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotEqual<T>( T expected, T actual, IEqualityComparer<T> comparer, object ctx ) {
			AreNotEqual( expected, actual, comparer, ctx, fmt: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotEqual<T>( T expected, T actual, IEqualityComparer<T> comparer, string fmt, params object[] args ) {
			AreNotEqual( expected, actual, comparer, ctx: null, fmt: fmt, args: args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotEqual<T>( T expected, T actual, IEqualityComparer<T> comparer, object ctx, string fmt, params object[] args ) {
			try {
				string message = string.IsNullOrEmpty( fmt ) ? null : string.Format( fmt, args );
				UEAssert.AreNotEqual( expected, actual, message, comparer );
			} catch( Exception exc ) {
				Throw( ctx, exc );
			}
		}
		#endregion

		#region AreApproxEqual
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreApproxEqual( float expected, float actual ) {
			AreApproxEqual( expected, actual, 0.00001f, null, fmt: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreApproxEqual( float expected, float actual, object ctx ) {
			AreApproxEqual( expected, actual, 0.00001f, ctx, fmt: null, args: null );
		}
		
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreApproxEqual( float expected, float actual, string fmt, params object[] args ) {
			AreApproxEqual( expected, actual, 0.00001f, ctx: null, fmt: fmt, args: args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreApproxEqual( float expected, float actual, object ctx, string fmt, params object[] args ) {
			AreApproxEqual( expected, actual, 0.00001f, ctx, fmt, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreApproxEqual( float expected, float actual, float tolerance, string fmt, params object[] args ) {
			AreApproxEqual( expected, actual, tolerance, ctx: null, fmt: fmt, args: args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreApproxEqual( float expected, float actual, float tolerance, object ctx, string fmt, params object[] args ) {
			try {
				string message = string.IsNullOrEmpty( fmt ) ? null : string.Format( fmt, args );
				UEAssert.AreApproximatelyEqual( expected, actual, tolerance, message );
			} catch( Exception exc ) {
				Throw( ctx, exc );
			}
		}
		#endregion

		#region AreNotApproxEqual
		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotApproxEqual( float expected, float actual ) {
			AreNotApproxEqual( expected, actual, 0.00001f, null, fmt: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotApproxEqual( float expected, float actual, object ctx ) {
			AreNotApproxEqual( expected, actual, 0.00001f, ctx, fmt: null, args: null );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotApproxEqual( float expected, float actual, string fmt, params object[] args ) {
			AreNotApproxEqual( expected, actual, 0.00001f, ctx: null, fmt: fmt, args: args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotApproxEqual( float expected, float actual, object ctx, string fmt, params object[] args ) {
			AreNotApproxEqual( expected, actual, 0.00001f, ctx, fmt, args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotApproxEqual( float expected, float actual, float tolerance, string fmt, params object[] args ) {
			AreNotApproxEqual( expected, actual, tolerance, ctx: null, fmt: fmt, args: args );
		}

		[Conditional( "DEBUG_ASSERTIONS" )]
		public static void AreNotApproxEqual( float expected, float actual, float tolerance, object ctx, string fmt, params object[] args ) {
			try {
				string message = string.IsNullOrEmpty( fmt ) ? null : string.Format( fmt, args );
				UEAssert.AreNotApproximatelyEqual( expected, actual, tolerance, message );
			} catch( Exception exc ) {
				Throw( ctx, exc );
			}
		}
		#endregion

		#region Throwing
		private static void Throw( object ctx, Exception exc ) {
			AddLogEntry( LogType.Assertion, ctx, exc );
			throw exc; // Rethrow
		}
		#endregion
	}
}
#if COMPILE_NAMESPACE
}
#endif