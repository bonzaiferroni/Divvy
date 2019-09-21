using Bonwerk.Divvy.Elements;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Editor
{
    public static class ContextFunctions
    {
        // Add a menu item called "Double Mass" to a Rigidbody's context menu.
        [MenuItem("CONTEXT/ButtonElement/Add Background")]
        [MenuItem("CONTEXT/ImageElement/Add Background")]
        [MenuItem("CONTEXT/Div/Add Background")]
        private static void AddBackground(MenuCommand command)
        {
            var mono = (MonoBehaviour) command.context;
            if (mono.GetComponent<Graphic>()) return;
            var image = mono.gameObject.AddComponent<Image>();
            image.color = Color.black;
            image.type = Image.Type.Sliced;
            Undo.RegisterCreatedObjectUndo(image, "Add Background");
        }
    }
}