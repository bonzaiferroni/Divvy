using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class CanvasRevealer : ElementRevealer
    {
        public CanvasRevealer(IElement element, CanvasGroup canvasGroup, float animationTime, bool easeAnimation) : 
            base(element, animationTime, easeAnimation)
        {
            CanvasGroup = canvasGroup;
        }

        private CanvasGroup CanvasGroup { get; }

        public override bool InstantType => false;

        protected override void Modify(float amount)
        {
            CanvasGroup.alpha = amount;
            if (CanvasGroup.blocksRaycasts != IsVisible) CanvasGroup.blocksRaycasts = IsVisible;
        }
    }
}