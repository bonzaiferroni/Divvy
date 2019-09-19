using Bonwerk.Divvy.Elements;
using UnityEngine;

namespace Bonwerk.Divvy.Visibility
{
    [RequireComponent(typeof(TextElement))]
    public class DivFadeText : DivAnimatedVisibility
    {
        public TextElement Label { get; private set; }
        
        public override void Init()
        {
            Label = GetComponent<TextElement>(); // needs to come before base.Init() because Label is referenced
            base.Init();
        }

        protected override void Modify(float amount)
        {
            Label.Alpha = amount;
            Label.RaycastTarget = IsVisible;
        }
    }
}