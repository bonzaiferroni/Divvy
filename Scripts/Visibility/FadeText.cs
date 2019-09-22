using Bonwerk.Divvy.Elements;
using UnityEngine;

namespace Bonwerk.Divvy.Visibility
{
    [RequireComponent(typeof(TextElement))]
    public class FadeText : AnimatedVisibility
    {
        [SerializeField] private TextElement _label;
        private TextElement Label => _label;

        protected override void Modify(float amount)
        {
            Label.Alpha = amount;
            Label.RaycastTarget = IsVisible;
        }
    }
}