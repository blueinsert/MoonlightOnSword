using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluxEditor;
using Flux;
using UnityEditor;

[CustomEditor(typeof(FVelSetEvent), true)]
public class FVelSetEventInspector : FEventInspector
{
    public SerializedProperty m_relativeType;
    public SerializedProperty m_VX;
    public SerializedProperty m_VY;
    public SerializedProperty m_VZ;

    private FVelSetEvent m_obj = null;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_relativeType = serializedObject.FindProperty("Relative");
        m_VX = serializedObject.FindProperty("VX");
        m_VY = serializedObject.FindProperty("VY");
        m_VZ = serializedObject.FindProperty("VZ");
        m_obj = (FVelSetEvent)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.PropertyField(m_relativeType);
        if (m_obj.Relative != VelRelativeType.FromHit)
        {
            EditorGUILayout.PropertyField(m_VX);
            EditorGUILayout.PropertyField(m_VY);
            EditorGUILayout.PropertyField(m_VZ);
        }

        serializedObject.ApplyModifiedProperties();
    }

}
