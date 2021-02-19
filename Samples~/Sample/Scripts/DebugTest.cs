using UnityEngine;
using UE = UnityEngine;
using System.Collections;

public class DebugTest : MonoBehaviour, IDebugSquelcher {
	#region Fields
	private static readonly DebugContext STATIC_CONTEXT = Dbg.Context( "I'm not an Unity object" );
	
	private DefaultDebugSystem debugSystem;
	#endregion

	#region Mono
	private void OnEnable() {
		debugSystem = new DefaultDebugSystem();
		Dbg.DebugSystem = debugSystem;
		Dbg.Assert.RaiseExceptions = true;

		// Log
		Dbg.Log( STATIC_CONTEXT, "Log {0}", "hello" );
		Dbg.Log( "Dbg.Log(msg)" );
		Dbg.Log( this, "Dbg.Log(ctx, msg)" );
		Dbg.LogIf( true, "Dbg.LogIf(cond, msg)" );
		Dbg.LogIf( true, this, "Dbg.LogIf(cond, ctx, msg)" );

		Dbg.LogRelease( "Dbg.LogRelease(msg)" );
		Dbg.LogRelease( this, "Dbg.LogRelease(ctx, msg)" );
		Dbg.LogReleaseIf( true, "Dbg.LogReleaseIf(cond, msg)" );
		Dbg.LogReleaseIf( true, this, "Dbg.LogReleaseIf(cond, ctx, msg)" );

		// LogWarn
		Dbg.LogWarn( "Dbg.LogWarn(msg)" );
		Dbg.LogWarn( this, "Dbg.LogWarn(ctx, msg)" );
		Dbg.LogWarnIf( true, "Dbg.LogWarnIf(cond, msg)" );
		Dbg.LogWarnIf( true, this, "Dbg.LogWarnIf(cond, ctx, msg)" );

		Dbg.LogWarnRelease( "Dbg.LogWarnRelease(msg)" );
		Dbg.LogWarnRelease( this, "Dbg.LogWarnRelease(ctx, msg)" );
		Dbg.LogWarnReleaseIf( true, "Dbg.LogWarnReleaseIf(cond, msg)" );
		Dbg.LogWarnReleaseIf( true, this, "Dbg.LogWarnReleaseIf(cond, ctx, msg)" );

		// LogError
		Dbg.LogError( "Dbg.LogError(msg)" );
		Dbg.LogError( this, "Dbg.LogError(ctx, msg)" );
		Dbg.LogErrorIf( true, "Dbg.LogErrorIf(cond, msg)" );
		Dbg.LogErrorIf( true, this, "Dbg.LogErrorIf(cond, ctx, msg)" );

		Dbg.LogErrorRelease( "Dbg.LogErrorRelease(msg)" );
		Dbg.LogErrorRelease( this, "Dbg.LogErrorRelease(ctx, msg)" );
		Dbg.LogErrorReleaseIf( true, "Dbg.LogErrorReleaseIf(cond, msg)" );
		Dbg.LogErrorReleaseIf( true, this, "Dbg.LogErrorReleaseIf(cond, ctx, msg)" );

		Dbg.Log( this, "Hello" );
		Dbg.Log( this, "Dbg.Log" );
		Dbg.LogWarn( this, "Dbg.LogWarn" );
		Dbg.LogError( this, "Dbg.LogError" );

		Dbg.Assert.IsTrue( true, this, "true should be true" );
		try {
			Dbg.Assert.IsTrue( false, this, "true should not be false" );
		} catch( System.Exception exc ) {
			Dbg.LogError( this, "Dbg.LogError in catch" );
			Dbg.LogExc( this, exc );
		}

		InvokeRepeating( "Spam", 0.5f, 0.5f );
		InvokeRepeating( "ImportantMessage", 5.0f, 3.0f );
	}

	private void OnDisable() {
		CancelInvoke( "Spam" );
		CancelInvoke( "ImportantMessage" );
	}
	#endregion

	#region IDebugSquelcher
	public bool ShouldSquelchLog( LogType logType, System.Exception exc, Dbg.Message message ) {
		return logType == LogType.Log;
	}
	#endregion

	#region Logging
	private void Spam() {
		Dbg.Log( this, "Spam" );
	}

	private void ImportantMessage() {
		Dbg.LogError( this, "Ah! Something bad happened" );
	}
	#endregion

	#region GUI
	void OnGUI() {
		foreach( var log in debugSystem.Logs ) {
			UE.Object obj = log.Object;
			GUILayout.Label( obj == null ? "<null>" : obj.name );
			foreach( var entry in log.Logs ) {
				DrawEntry( entry.Frame, entry.LogType, entry.Message );
			}
		}
	}

	void DrawEntry( int frame, LogType logType, string message ) {
		Color color = logType == LogType.Log ? Color.white :
			logType == LogType.Warning ? Color.yellow :
			Color.red;

		GUILayout.BeginHorizontal();
		GUILayout.Space( 20.0f );

		GUI.color = color;
		GUILayout.Label( string.Format( "{0}:", frame ), GUILayout.MinWidth( 60.0f ) );
		GUILayout.Label( message );
		GUI.color = Color.white;

		GUILayout.EndHorizontal();
	}
	#endregion
}
