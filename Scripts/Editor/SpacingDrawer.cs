using Bonwerk.Divvy.Data;

namespace Bonwerk.Divvy.Editor
{
    using UnityEditor;
    using UnityEngine;

    // IngredientDrawer
    [CustomPropertyDrawer(typeof(Spacing))]
    public class SpacingDrawer : PropertyDrawer
    {
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var width = position.width / 4;
            var top = new Rect(position.x, position.y, width - 5, position.height);
            var right = new Rect(position.x + 1 * width, position.y, width - 5, position.height);
            var bottom = new Rect(position.x + 2 * width, position.y, width - 5, position.height);
            var left = new Rect(position.x + 3 * width, position.y, width - 5, position.height);

            // Draw fields - pass GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(top, property.FindPropertyRelative("Top"), GUIContent.none);
            EditorGUI.PropertyField(right, property.FindPropertyRelative("Right"), GUIContent.none);
            EditorGUI.PropertyField(bottom, property.FindPropertyRelative("Bottom"), GUIContent.none);
            EditorGUI.PropertyField(left, property.FindPropertyRelative("Left"), GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}