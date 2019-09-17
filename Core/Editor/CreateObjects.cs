using TMPro;
using UnityEditor;
using UnityEngine;

namespace Bonwerk.Divvy.Core.Editor
{
    public static class CreateObjects
    {
        // Add a menu item to create custom GameObjects.
        // Priority 1 ensures it is grouped with the other menu items of the same kind
        // and propagated to the hierarchy dropdown and hierarchy context menus.
        [MenuItem("GameObject/Divvy/DivRoot", false, 10)]
        private static void CreateDivRoot(MenuCommand menuCommand)
        {
            // Create a custom game object
            GameObject go = new GameObject("DivRoot");
            go.AddComponent<Div>().LineHeight = 30;
            go.AddComponent<DivRoot>();
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
        
        [MenuItem("GameObject/Divvy/Div", false, 10)]
        private static void CreateDiv(MenuCommand menuCommand)
        {
            // Create a custom game object
            GameObject go = new GameObject("Div");
            go.AddComponent<Div>();
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
        
        [MenuItem("GameObject/Divvy/Text", false, 10)]
        private static void CreateText(MenuCommand menuCommand)
        {
            var parent = menuCommand.context as GameObject;
            var div = parent.GetComponent<Div>();
            if (!div)
            {
                Debug.Log("Element must have parent");
                return;
            }
            
            // Create a custom game object
            GameObject go = new GameObject("Text");
            go.AddComponent<DivText>();
            var text = go.AddComponent<TextMeshProUGUI>();
            text.text = "DivText";
            text.fontSize = div.LineHeight;
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, parent);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
}