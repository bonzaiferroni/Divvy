using Bonwerk.Divvy.Elements;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Core.Editor
{
    public static class ContextFunctions
    {
        // Add a menu item called "Double Mass" to a Rigidbody's context menu.
        [MenuItem("CONTEXT/Div/Add Background")]
        private static void AddBackground(MenuCommand command)
        {
            Div div = (Div) command.context;
            if (div.GetComponent<Graphic>()) return;
            var image = div.gameObject.AddComponent<Image>();
            image.color = Color.black;
            Undo.RegisterCreatedObjectUndo(image, "Add Background");
        }
    }
}