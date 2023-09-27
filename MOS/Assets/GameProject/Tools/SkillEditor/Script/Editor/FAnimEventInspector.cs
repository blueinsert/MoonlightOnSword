using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluxEditor;
using Flux;
using UnityEditor;

[CustomEditor(typeof(FAnimEvent), true)]
public class FAnimEventInspector : FEventInspector
{
    public SerializedProperty m_animName;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_animName = serializedObject.FindProperty("Anim");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //EditorGUILayout.PropertyField(m_animName);
        //serializedObject.ApplyModifiedProperties();
    }

}
