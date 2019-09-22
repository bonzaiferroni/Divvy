using Bonwerk.Divvy.Elements;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Bonwerk.Divvy.Editor
{
    public static class CreateObjects
    {
        private static GameObject Create(string name)
        {
            // Create a custom game object
            var go = Object.Instantiate(Resources.Load<GameObject>($"Prefabs/{name}"));
            go.name = name;
            return go;
        }

        private static void Create(string name, MenuCommand menuCommand)
        {
            var parent = menuCommand.context as GameObject;
            if (!HasDivParent(parent)) return;
            var go = Create(name);
            Integrate(go, parent);
        }
        
        private static bool HasDivParent(GameObject parent)
        {
            if (!parent.GetComponent<DivElement>())
            {
                Debug.LogWarning("Element must have parent");
                return false;
            }
            return true;
        }

        private static void Integrate(GameObject go, GameObject parent)
        {
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, parent);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
        
        // Add a menu item to create custom GameObjects.
        // Priority 1 ensures it is grouped with the other menu items of the same kind
        // and propagated to the hierarchy dropdown and hierarchy context menus.
        [MenuItem("GameObject/Divvy/DivRoot", false, 1)]
        private static void DivRoot(MenuCommand menuCommand)
        {
            var go = Create("DivRoot");
            Integrate(go, menuCommand.context as GameObject);
        }
        
        [MenuItem("GameObject/Divvy/Div", false, 1)]
        private static void Div(MenuCommand menuCommand)
        {
            Create("Div", menuCommand);
        }
        
        [MenuItem("GameObject/Divvy/Text", false, 1)]
        private static void Text(MenuCommand menuCommand)
        {
            Create("Text", menuCommand);
        }
        
        [MenuItem("GameObject/Divvy/Header", false, 1)]
        private static void Header(MenuCommand menuCommand)
        {
            Create("Header", menuCommand);
        }
        
        [MenuItem("GameObject/Divvy/TextButton", false, 1)]
        private static void TextButton(MenuCommand menuCommand)
        {
            Create("TextButton", menuCommand);
        }
        
        [MenuItem("GameObject/Divvy/SpriteButton", false, 1)]
        private static void SpriteButton(MenuCommand menuCommand)
        {
            Create("SpriteButton", menuCommand);
        }
    }
}