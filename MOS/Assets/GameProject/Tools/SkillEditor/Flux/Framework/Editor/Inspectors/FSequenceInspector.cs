using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

using Flux;
using FluxEditor;

namespace FluxEditor
{
	[CustomEditor(typeof(FSequence))]
	public class FSequenceInspector : Editor {

		private FSequence _sequence;

		void OnEnable()
		{
			_sequence = (FSequence)target;
		}

		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI();

			serializedObject.Update();

			EditorGUILayout.Space();

			if( GUILayout.Button( "Open In Flux Editor" ) )
			{
				FSequenceEditorWindow.Open( _sequence );
			}

			EditorGUILayout.Space();

			serializedObject.ApplyModifiedProperties();
		}

	}
}
