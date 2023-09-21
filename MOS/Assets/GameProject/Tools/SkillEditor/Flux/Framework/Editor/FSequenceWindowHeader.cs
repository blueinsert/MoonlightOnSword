using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

using Flux;

namespace FluxEditor
{
    public class FSequenceWindowHeader
    {
        // padding on top, bottom, left and right
        private const float PADDING = 5;
        // space between labels and the fields
        private const float LABEL_SPACE = 5;
        // space between elements (i.e. label+field pairs)
        private const float ELEMENT_SPACE = 20;
        // height of the header
        public const float HEIGHT = 20 + PADDING + PADDING;
        private const float MAX_SEQUENCE_POPUP_WIDTH = 250;
        private const float FRAMERATE_FIELD_WIDTH = 40;
        private const float LENGTH_FIELD_WIDTH = 100;

        private FSequenceEditorWindow _sequenceWindow;
        private SerializedObject _sequenceSO;
        private SerializedProperty _sequenceLength;

        private GUIContent _sequenceNameLabel = new GUIContent("Sequence:", "Cur Editing Sequence");
        private GUIContent _sequenceNameField = new GUIContent("the example skill name", "The Sequence's Name");
        private Rect _sequenceNameLabelRect;
        private Rect _sequenceNameFieldRect;

        //add Container
        private GUIContent _addContainerLabel = new GUIContent(string.Empty, "Add Container To Sequence");
        private Rect _addContainerRect;

        private  GUIContent _playLabel = new GUIContent("Play", "play the sequence");
        private Rect _playRect;

        // length UI variables
        private GUIContent _lengthLabel = new GUIContent("Length", "What's the length of the sequence");
        private Rect _lengthLabelRect;
        private Rect _lengthFieldRect;

        // open inspector
        private GUIContent _openInspectorLabel = new GUIContent(string.Empty, "Open Flux Inspector");
        private Rect _openInspectorRect;

        // cached number field style, since we want numbers centered
        private GUIStyle _numberFieldStyle;

        public FSequenceWindowHeader(FSequenceEditorWindow sequenceWindow)
        {
            _sequenceWindow = sequenceWindow;

            EditorApplication.hierarchyWindowChanged += OnHierarchyChanged;
            //EditorApplication.hierarchyChanged += OnHierarchyChanged;

            _addContainerLabel.image = FUtility.GetFluxTexture("AddFolder.png");
            _openInspectorLabel.image = FUtility.GetFluxTexture("Inspector.png");
        }

        private void OnHierarchyChanged()
        {
        }



        public void RebuildLayout(Rect rect)
        {
            rect.xMin += PADDING;
            rect.yMin += PADDING;
            rect.xMax -= PADDING;
            rect.yMax -= PADDING;

            float width = rect.width;

            _playRect = rect;
            _openInspectorRect = rect;
            _lengthLabelRect = _lengthFieldRect = rect;
            _sequenceNameLabelRect = rect;
            _sequenceNameFieldRect = rect;

            _sequenceNameLabelRect.width = EditorStyles.label.CalcSize(_sequenceNameLabel).x + LABEL_SPACE;
            _sequenceNameFieldRect.width = EditorStyles.label.CalcSize(_sequenceNameField).x + LABEL_SPACE;
            _playRect.width = EditorStyles.label.CalcSize(_playLabel).x + LABEL_SPACE;
            _lengthLabelRect.width = EditorStyles.label.CalcSize(_lengthLabel).x + LABEL_SPACE;
            _lengthFieldRect.width = LENGTH_FIELD_WIDTH;
            _addContainerRect = new Rect(0, 3, 22, 22);

            _sequenceNameLabelRect.x = rect.xMin + LABEL_SPACE;
            _sequenceNameFieldRect.x = _sequenceNameLabelRect.xMax + LABEL_SPACE;
            _addContainerRect.x = _sequenceNameFieldRect.xMax + LABEL_SPACE;
            _openInspectorRect.xMin = _openInspectorRect.xMax - 22;
            _lengthFieldRect.x = rect.xMax - 22 - PADDING - _lengthFieldRect.width;
            _lengthLabelRect.x = _lengthFieldRect.xMin - _lengthLabelRect.width;
            _playRect.x = _lengthLabelRect.xMin - _playRect.width - PADDING;
            _numberFieldStyle = new GUIStyle(EditorStyles.numberField);
            _numberFieldStyle.alignment = TextAnchor.MiddleCenter;
        }

        public void OnGUI()
        {
            FSequence sequence = _sequenceWindow.GetSequenceEditor().Sequence;

            if (sequence == null)
                return;

            if (_sequenceSO == null || _sequenceSO.targetObject != sequence)
            {
                _sequenceSO = new SerializedObject(sequence);
                _sequenceLength = _sequenceSO.FindProperty("_length");
            }
            _sequenceSO.Update();

            EditorGUI.PrefixLabel(_lengthLabelRect, _lengthLabel);
            _sequenceLength.intValue = Mathf.Clamp(EditorGUI.IntField(_lengthFieldRect, _sequenceLength.intValue, _numberFieldStyle), 1, int.MaxValue);

            GUIStyle s = new GUIStyle(EditorStyles.miniButton);
            s.padding = new RectOffset(1, 1, 1, 1);

            _sequenceNameField.text = sequence.name;
            //EditorGUI.LabelField()
            EditorGUI.LabelField(_sequenceNameLabelRect, _sequenceNameLabel);
            EditorGUI.LabelField(_sequenceNameFieldRect, _sequenceNameField);
            //EditorGUI.ObjectField(_sequenceNameFieldRect, _sequenceSO);
            if (FGUI.Button(_addContainerRect, _addContainerLabel))
            {
                AddContainer();
            }

            if (FGUI.Button(_openInspectorRect, _openInspectorLabel))
            {
                FInspectorWindow.Open();
            }

            if (GUI.Button(_playRect, _playLabel, s))
            {
                Play(sequence);
            }
            
            _sequenceSO.ApplyModifiedProperties();

            GUI.enabled = true;
        }

        private void AddContainer()
        {
            _sequenceWindow.GetSequenceEditor().CreateContainer();
        }

        private void Play(FSequence sequence)
        {
            if (!Application.isPlaying)
            {
                Debug.LogError("Application is not Playing");
                return;
            }
            foreach(var skillComp in GameObject.FindObjectsOfType<BehaviorSkillComp>())
            {
                bool isContain = false;
                foreach(var skill in skillComp.m_skillsDesc.m_skillList)
                {
                    if(skill.ID == sequence.ID)
                    {
                        isContain = true;
                    }
                }
                if (isContain)
                {
                    skillComp.ForcePlaySkill(sequence.ID);
                }
            }
            _sequenceWindow.GetSequenceEditor().Play();
        }

    }
}
