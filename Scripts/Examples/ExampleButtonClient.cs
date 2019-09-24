using System;
using Bonwerk.Divvy.Elements;
using UnityEngine;

namespace Bonwerk.Divvy.Examples
{
    public class ExampleButtonClient : MonoBehaviour
    {
        public ButtonElement Button;
        public Element Target;
        public Bar Foo;

        private void Start()
        {
            if (!Button || !Target) return;
            Button.AddListener(ToggleTarget);
        }

        private void ToggleTarget()
        {
            Target.Revealer.Toggle();
        }
    }

    [Serializable]
    public class Foo
    {
        [Header("Sizing")]
        public Vector2 Size;
    }

    [Serializable]
    public class Bar : Foo
    {
        [Header("Position")] 
        public Vector2 Position;
    }
}