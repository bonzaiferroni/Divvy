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
        private DivvyElement _element;

        void OnEnable ()
        {
            _element = target as DivvyElement;
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