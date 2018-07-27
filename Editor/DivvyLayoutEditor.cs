using Divvy.Core;
using UnityEditor;
using UnityEngine;

namespace Divvy
{
    [CanEditMultipleObjects]
    // [CustomEditor(typeof(DivvyLayout))]
    public class DivvyLayoutEditor : Editor
    {
        private SerializedProperty _styleProperty;
        private DivvyPanel _panel;

        void OnEnable ()
        {
            _panel = target as DivvyPanel;
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