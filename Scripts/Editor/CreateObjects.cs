using Bonwerk.Divvy.Elements;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Bonwerk.Divvy.Editor
{
    public static class CreateObjects
    {
        // Add a menu item to create custom GameObjects.
        // Priority 1 ensures it is grouped with the other menu items of the same kind
        // and propagated to the hierarchy dropdown and hierarchy context menus.
        [MenuItem("GameObject/Divvy/DivRoot", false, 1)]
        private static void CreateDivRoot(MenuCommand menuCommand)
        {
            // Create a custom game object
            var go = Object.Instantiate(Resources.Load<GameObject>("Prefabs/DivRoot"));
            go.name = "DivRoot";
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
        
        [MenuItem("GameObject/Divvy/Div", false, 1)]
        private static void CreateDiv(MenuCommand menuCommand)
        {
            var parent = menuCommand.context as GameObject;
            if (!parent.GetComponent<Div>())
            {
                Debug.Log("Element must have parent");
                return;
            }
            // Create a custom game object
            var go = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Div"));
            go.name = "Div";
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, parent);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
        
        [MenuItem("GameObject/Divvy/Text", false, 1)]
        private static void CreateText(MenuCommand menuCommand)
        {
            var parent = menuCommand.context as GameObject;
            if (!parent.GetComponent<Div>())
            {
                Debug.Log("Element must have parent");
                return;
            }
            
            // Create a custom game object
            var go = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Text"));
            go.name = "Text";
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, parent);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
        
        [MenuItem("GameObject/Divvy/Header", false, 1)]
        private static void CreateHeader(MenuCommand menuCommand)
        {
            var parent = menuCommand.context as GameObject;
            if (!parent.GetComponent<Div>())
            {
                Debug.Log("Element must have parent");
                return;
            }
            
            // Create a custom game object
            var go = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Header"));
            go.name = "Header";
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, parent);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
}