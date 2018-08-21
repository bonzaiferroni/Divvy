﻿using DivLib.Core;
using UnityEditor;
using UnityEngine;

namespace Divvy
{
    [CanEditMultipleObjects]
    // [CustomEditor(typeof(DivvyLayout))]
    public class DivvyLayoutEditor : Editor
    {
        private SerializedProperty _styleProperty;
        private Element _panel;

        void OnEnable ()
        {
            _panel = target as Element;
            // Setup the SerializedProperties.
            _styleProperty = serializedObject.FindProperty ("Style");
        }
        
        public override void OnInspectorGUI() {
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update();
            // if (GUILayout.Button("SetDirty")) { }
            DrawDefaultInspector();
        }
    }
}