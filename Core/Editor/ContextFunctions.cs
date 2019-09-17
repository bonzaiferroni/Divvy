using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Core.Editor
{
    public static class ContextFunctions
    {
        // Add a menu item called "Double Mass" to a Rigidbody's context menu.
        [MenuItem("CONTEXT/Div/Add Background")]
        private static void DoubleMass(MenuCommand command)
        {
            Div div = (Div) command.context;
            if (div.GetComponent<Graphic>()) return;
            div.gameObject.AddComponent<Image>();
        }
    }
}