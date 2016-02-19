using UnityEngine;
using UnityEditor;
using UE = UnityEngine;
using System;
using System.Collections.Generic;

#if COMPILE_NAMESPACE
using SA;

namespace SA {
#endif
[InitializeOnLoad]
public static class DebugPreferences {
	#region Fields
	private static readonly string ASSERTIONS_COMPILE_DEFINE = "DEBUG_ASSERTIONS";
	private static readonly string LOGGING_COMPILE_DEFINE = "DEBUG_LOGGING";

	private static readonly bool UnityAssertionsEnabled =
#if UNITY_ASSERTIONS
		true;
#else
		false;
#endif

	private static readonly bool AssertionsEnabled =
#if DEBUG_ASSERTIONS
		true;
#else
		false;
#endif

	private static readonly bool LoggingEnabled =
#if DEBUG_LOGGING
		true;
#else
		false;
#endif

	private static bool gui_unityAssertions;
	private static bool gui_assertions;
	private static bool gui_logging;
	#endregion

	#region Constructor
	static DebugPreferences() {
		gui_unityAssertions = UnityAssertionsEnabled;
		gui_assertions = AssertionsEnabled;
		gui_logging = LoggingEnabled;
	}
	#endregion

	#region GUI
	[PreferenceItem( "Debugging" )]
	public static void PreferencesGUI() {
		if( EditorApplication.isPlaying || EditorApplication.isCompiling || EditorApplication.isUpdating ) {
			EditorGUILayout.HelpBox( "Editor is busy.", MessageType.Info );
			return;
		}

		BuildTargetGroup activeTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
		string activeTargetGroupName = Enum.GetName( typeof(BuildTargetGroup), activeTargetGroup );
		EditorGUILayout.HelpBox( "Settings for " + activeTargetGroupName, MessageType.Info );

		if( gui_assertions && !gui_unityAssertions ) {
			EditorGUILayout.HelpBox( "Unity assertions need to be enabled for debug assertions to work.", MessageType.Warning );
		}

		EditorGUI.BeginChangeCheck();
		EditorGUI.BeginDisabledGroup( disabled: true );
		gui_unityAssertions = EditorGUILayout.Toggle( "Unity Assertions", gui_unityAssertions );
		EditorGUI.EndDisabledGroup();
		gui_assertions = EditorGUILayout.Toggle( "Assertions", gui_assertions );
		gui_logging = EditorGUILayout.Toggle( "Logging", gui_logging );
		bool enabled = gui_assertions != AssertionsEnabled || gui_logging != LoggingEnabled;
		EditorGUI.BeginDisabledGroup( !enabled );
		if( GUILayout.Button( "Apply" ) ) {
			Apply();
		}
		EditorGUI.EndDisabledGroup();
	}
	#endregion

	#region Applying
	private static void Apply() {
		if( EditorApplication.isPlaying || EditorApplication.isCompiling || EditorApplication.isUpdating ) {
			return;
		}

		BuildTargetGroup activeTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
		string[] activeDefines = PlayerSettings.GetScriptingDefineSymbolsForGroup( activeTargetGroup ).Split( ';' );
		var defines = new List<string>( activeDefines );
		
		if( gui_assertions && !defines.Contains( ASSERTIONS_COMPILE_DEFINE ) ) {
			defines.Add( ASSERTIONS_COMPILE_DEFINE );
		}
		
		if( !gui_assertions ) {
			defines.Remove( ASSERTIONS_COMPILE_DEFINE );
		}

		if( gui_logging && !defines.Contains( LOGGING_COMPILE_DEFINE ) ) {
			defines.Add( LOGGING_COMPILE_DEFINE );
		}

		if( !gui_logging ) {
			defines.Remove( LOGGING_COMPILE_DEFINE );
		}

		string newDefines = string.Join( ";", defines.ToArray() );
		PlayerSettings.SetScriptingDefineSymbolsForGroup( activeTargetGroup, newDefines );
	}
	#endregion
}
#if COMPILE_NAMESPACE
}
#endif