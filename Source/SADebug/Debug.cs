using UnityEngine;
using UE = UnityEngine;
using System;
using System.Diagnostics;
using System.Collections.Generic;

#if COMPILE_NAMESPACE
using SA;

namespace SA {
#endif
public static partial class Dbg {
	#region Fields
	// Will receive log hooks with logged messages.
	public static IDebugSystem DebugSystem;

	private static Message TempMessage = new Message();

	static readonly HashSet<OnceKey> onceLookup = new HashSet<OnceKey>(new OnceKey.EqualityComparer());
	#endregion

	#region Break
	[Conditional( "UNITY_EDITOR" )]
	public static void Break() {
		UE.Debug.Break();
	}

	[Conditional("UNITY_EDITOR")]
	public static void BreakIf( bool cond ) {
		if( cond ) { UE.Debug.Break(); }
	}
	#endregion

	#region Once Tokens
	// More or less an object and a string identifier
	// Will _either_ contain a strong or a weak reference to the object,
	// to avoid a new() for every lookup
	struct OnceKey
	{
		public class EqualityComparer : IEqualityComparer<OnceKey>
		{
			public bool Equals(OnceKey x, OnceKey y)
			{
				return x.target == y.target && x.identifier == y.identifier;
			}

			public int GetHashCode(OnceKey key)
			{
				return key.hashCode;
			}
		}

		readonly int hashCode;
		readonly WeakReference weakRef;
		readonly object strongRef;
		readonly string identifier;

		public object target
		{
			get { return strongRef != null ? strongRef : weakRef.Target; }
		}

		public OnceKey(object obj, string identifier, bool createRef)
		{
			this.hashCode = CombineHashes(obj.GetHashCode(), identifier.GetHashCode());
			this.weakRef = createRef ? new WeakReference(obj) : null;
			this.strongRef = createRef ? null : obj;
			this.identifier = identifier;
		}

		public override int GetHashCode()
		{
			return hashCode;
		}

		// TODO: This is not a good spot for this
		// Kudoes: http://stackoverflow.com/questions/1646807/quick-and-simple-hash-code-combinations
		static int CombineHashes(int h1, int h2)
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 31 + h1;
				hash = hash * 31 + h2;
				return hash;
			}
		}
	}

	public static void ResetOnceTokens()
	{
		onceLookup.Clear();
	}

	static bool CheckOnceToken(DebugContext ctx, string identifier)
	{
		if(ctx.obj == null) { return false; }

		var kvp = new OnceKey(ctx.obj, identifier, createRef: false);

		if(!onceLookup.Contains(kvp))
		{
			kvp = new OnceKey(ctx.obj, identifier, createRef: true);
			onceLookup.Add(kvp);
			return true;
		}

		return false;
	}
	#endregion // Once Tokens

	#region Boxing
	public class Message {
		public string stringValue {
			get {
				if( str == null ) {
					str = args == null ? fmt : string.Format(fmt, args);
				}
				return str;
			}
		}

		public static implicit operator string( Message msg ) {
			return msg.stringValue;
		}

		internal string str;
		internal string fmt;
		internal object[] args;

		internal void Reset( string fmt, object[] args ) {
			this.fmt = fmt;
			this.args = args;
			this.str = null;
		}
	}

	public static DebugContext Context( object ctx, UE.Object ueCtx = null ) {
		return new DebugContext( ctx, ueCtx );
	}
	#endregion

	#region Debug System Hook
	private static void AddLogEntry( LogType logType, DebugContext ctx, Exception exc ) {
		AddLogEntry( logType, ctx, exc, fmt: null, args: null );
	}

	private static void AddLogEntry( LogType logType, DebugContext ctx, string fmt, params object[] args ) {
		AddLogEntry( logType, ctx, exc: null, fmt: fmt, args: args );
	}

	private static void AddLogEntry( LogType logType, DebugContext ctx, Exception exc, string fmt, params object[] args ) {
		TempMessage.Reset( fmt, args );
		
		bool squelch = false;

		if( DebugSystem != null ) {
			DebugSystem.AddLogEntry( logType, ctx, exc, TempMessage, out squelch );
		}

		if( ctx.obj is IDebugSquelcher ) {
			squelch |= ( (IDebugSquelcher)ctx.obj ).ShouldSquelchLog( logType, exc, TempMessage );
		}

		if( squelch ) {
			return;
		}

		string message = TempMessage.stringValue;
		var ueCtx = ctx.unityObj;

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