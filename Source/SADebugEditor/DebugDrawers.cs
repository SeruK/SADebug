using UnityEngine;
using UE = UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

#if COMPILE_NAMESPACE
using SA;

namespace SA {
#endif
public static class DbgDraw {
	private static readonly List<Keyframe> KEYFRAMES_BUFFER = new List<Keyframe>( 10 );
	private static readonly AnimationCurve ANIMATION_CURVE = new AnimationCurve();

	// Kudos http://stackoverflow.com/questions/374651/how-to-check-if-an-object-is-nullable
	private static bool IsNullable<T>( T obj ) {
		if( obj == null ) return true; // obvious
		Type type = typeof( T );
		if( !type.IsValueType ) return true; // ref-type
		if( Nullable.GetUnderlyingType( type ) != null ) return true; // Nullable<T>
		return false; // value-type
	}

	internal class EditorDrawer<TVal> : Dbg.IRecordedValueDrawer<TVal> {
		private static readonly GUIContent GUI_CONTENT = new GUIContent();
		private bool expanded;

		public TVal recordedValue {
			get; internal set;
		}

		public void Draw( string name, IList<Dbg.Frame<TVal>> frames ) {
			GUI_CONTENT.text = name;
			GUILayout.Label( GUI_CONTENT );
			DrawContent( frames );
		}

		protected virtual void DrawContent( IList<Dbg.Frame<TVal>> frames ) {
			var currentFrame = frames[ frames.Count - 1 ];
			GUI_CONTENT.text = currentFrame.value.ToString();
			GUILayout.Label( GUI_CONTENT );
		}
	}

	internal class PrimitiveDrawer<TVal> : EditorDrawer<TVal> {
		protected override void DrawContent( IList<Dbg.Frame<TVal>> frames ) {
			DrawFrames( frames );
			base.DrawContent( frames );
		}
	}

	private static readonly PrimitiveDrawer<int> INT_DRAWER = new PrimitiveDrawer<int>();
	private static readonly PrimitiveDrawer<float> FLOAT_DRAWER = new PrimitiveDrawer<float>();

	private static void DrawFrames<TVal>( IList<Dbg.Frame<TVal>> frames ) {
		while( KEYFRAMES_BUFFER.Count < frames.Count ) {
			KEYFRAMES_BUFFER.Add( new Keyframe() );
		}

		while( KEYFRAMES_BUFFER.Count > frames.Count ) {
			KEYFRAMES_BUFFER.RemoveAt( 0 );
		}

		for( int i = 0; i < frames.Count; ++i ) {
			float time = i / (float)frames.Count;
			Dbg.Frame<TVal> frame = frames[ i ];
			float value = Convert.ToSingle( frame.value );

			KEYFRAMES_BUFFER[ i ] = new Keyframe( time, value );
		}

		ANIMATION_CURVE.keys = KEYFRAMES_BUFFER.ToArray();
		UnityEditor.EditorGUILayout.CurveField( ANIMATION_CURVE );
	}

	public static Dbg.IRecordedValueDrawer<TVal> EditorGUILayout<TVal>( TVal value ) {
		EditorDrawer<TVal> drawer = null;
		if( typeof( TVal ) == typeof( int ) ) { drawer = INT_DRAWER as EditorDrawer<TVal>; }
		else if( typeof( TVal ) == typeof( float ) ) { drawer = FLOAT_DRAWER as EditorDrawer<TVal>; }
		else { drawer = new EditorDrawer<TVal>(); }
		drawer.recordedValue = value;
		return drawer;
	}
}
#if COMPILE_NAMESPACE
}
#endif