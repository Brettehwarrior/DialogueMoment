using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Ok so I may have copied a lot here
// https://github.com/mixandjam/AC-Dialogue/blob/master/Assets/TMP_Animated/Editor/TMP_AnimatedEditor.cs
// But I needed to be able to see the things in the inspector
namespace TMPro.EditorUtilities{
    [CustomEditor(typeof(AnimatedTMP), true)]
    [CanEditMultipleObjects]
    public class AnimatedTMPEditor : TMP_BaseEditorPanel
    {
        SerializedProperty speedProp;
        SerializedProperty emotionProp;

        protected override void OnEnable()
        {
            base.OnEnable();
            speedProp = serializedObject.FindProperty("characterDelay");
            emotionProp = serializedObject.FindProperty("onTextScroll");

        }
        protected override void DrawExtraSettings()
        {
            EditorGUILayout.LabelField("Animation Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(speedProp, new GUIContent("     Default Speed"));
            EditorGUILayout.PropertyField(emotionProp);
        }

        protected override void OnUndoRedo()
        {
            // haha i do not know
        }

        protected override bool IsMixSelectionTypes()
        {
            return false;
        }
    }
}