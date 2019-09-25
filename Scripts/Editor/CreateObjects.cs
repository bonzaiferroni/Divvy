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
            if (!parent.GetComponent<Div>())
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
        [MenuItem("GameObject/DivvyUI/DivRoot", false, 0)]
        private static void DivRoot(MenuCommand menuCommand)
        {
            var go = Create("DivRoot");
            Integrate(go, menuCommand.context as GameObject);
        }
        
        [MenuItem("GameObject/DivvyUI/Div", false, 0)]
        private static void Div(MenuCommand menuCommand)
        {
            Create("Div", menuCommand);
        }
        
        [MenuItem("GameObject/DivvyUI/Text", false, 0)]
        private static void Text(MenuCommand menuCommand)
        {
            Create("Text", menuCommand);
        }
        
        [MenuItem("GameObject/DivvyUI/Header", false, 0)]
        private static void Header(MenuCommand menuCommand)
        {
            Create("Header", menuCommand);
        }
        
        [MenuItem("GameObject/DivvyUI/TextButton", false, 0)]
        private static void TextButton(MenuCommand menuCommand)
        {
            Create("TextButton", menuCommand);
        }
        
        [MenuItem("GameObject/DivvyUI/Image", false, 0)]
        private static void Image(MenuCommand menuCommand)
        {
            Create("Image", menuCommand);
        }
        
        [MenuItem("GameObject/DivvyUI/SpriteButton", false, 0)]
        private static void SpriteButton(MenuCommand menuCommand)
        {
            Create("SpriteButton", menuCommand);
        }
        
        [MenuItem("GameObject/DivvyUI/LineBreak", false, 0)]
        private static void LineBreak(MenuCommand menuCommand)
        {
            Create("LineBreak", menuCommand);
        }
        
        [MenuItem("GameObject/DivvyUI/InputField", false, 0)]
        private static void InputField(MenuCommand menuCommand)
        {
            Create("InputField", menuCommand);
        }
        
        [MenuItem("GameObject/DivvyUI/DivScroll", false, 0)]
        private static void DivScroll(MenuCommand menuCommand)
        {
            Create("DivScroll", menuCommand);
        }
        
        [MenuItem("GameObject/DivvyUI/ComboToggle", false, 0)]
        private static void ComboToggle(MenuCommand menuCommand)
        {
            Create("ComboToggle", menuCommand);
        }
        
        [MenuItem("GameObject/DivvyUI/TextToggle", false, 0)]
        private static void TextToggle(MenuCommand menuCommand)
        {
            Create("TextToggle", menuCommand);
        }
        
        [MenuItem("GameObject/DivvyUI/SpriteToggle", false, 0)]
        private static void SpriteToggle(MenuCommand menuCommand)
        {
            Create("SpriteToggle", menuCommand);
        }
        
        [MenuItem("GameObject/DivvyUI/Slider", false, 0)]
        private static void Slider(MenuCommand menuCommand)
        {
            Create("Slider", menuCommand);
        }
        
        [MenuItem("GameObject/DivvyUI/ToggleDiv", false, 0)]
        private static void ToggleDiv(MenuCommand menuCommand)
        {
            Create("ToggleDiv", menuCommand);
        }
        
        [MenuItem("GameObject/DivvyUI/BookDiv", false, 0)]
        private static void BookDiv(MenuCommand menuCommand)
        {
            Create("BookDiv", menuCommand);
        }
    }
}