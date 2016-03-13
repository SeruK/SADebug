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

	private static List<string> ActiveDefines {
		get {
			BuildTargetGroup activeTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
			string[] activeDefines = PlayerSettings.GetScriptingDefineSymbolsForGroup( activeTargetGroup ).Split( ';' );
			return new List<string>( activeDefines );
		}

		set {
			BuildTargetGroup activeTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
			string definesString = value == null ? "" : string.Join( ";", value.ToArray() );
			PlayerSettings.SetScriptingDefineSymbolsForGroup( activeTargetGroup, definesString );
		}
	}

	private static bool gui_assertions;
	private static bool gui_logging;
	#endregion

	#region Constructor
	static DebugPreferences() {
		List<string> activeDefines = ActiveDefines;
		gui_assertions = activeDefines.Contains( ASSERTIONS_COMPILE_DEFINE );
		gui_logging    = activeDefines.Contains( LOGGING_COMPILE_DEFINE );
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

		string[] activeDefines = EditorUserBuildSettings.activeScriptCompilationDefines;
		bool assertionsEnabled = -1 != Array.IndexOf( activeDefines, ASSERTIONS_COMPILE_DEFINE );
		bool loggingEnabled = -1 != Array.IndexOf( activeDefines, LOGGING_COMPILE_DEFINE );

		EditorGUI.BeginChangeCheck();
		gui_assertions = EditorGUILayout.Toggle( "Assertions", gui_assertions );
		gui_logging = EditorGUILayout.Toggle( "Logging", gui_logging );
		bool enabled = gui_assertions != assertionsEnabled || gui_logging != loggingEnabled;
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

		List<string> defines = ActiveDefines;
		
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

		ActiveDefines = defines;
	}
	#endregion
}
#if COMPILE_NAMESPACE
}
#endif