using System;
using Bonwerk.Divvy.Elements;
using UnityEngine;

namespace Bonwerk.Divvy.Examples
{
    public class ExampleButtonClient : MonoBehaviour
    {
        public ButtonElement Button;
        public Element Target;

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
}