using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class CanvasGroupRevealer : ElementRevealer
    {
        public CanvasGroupRevealer(IElement element, CanvasGroup canvasGroup, float animationTime, bool easeAnimation) : 
            base(element, animationTime, easeAnimation)
        {
            CanvasGroup = canvasGroup;
        }

        private CanvasGroup CanvasGroup { get; }

        public override bool InstantType => false;

        protected override float FindInitialState()
        {
            return CanvasGroup.alpha;
        }

        protected override void Modify(float amount)
        {
            CanvasGroup.alpha = amount;
            if (CanvasGroup.blocksRaycasts != IsVisible) CanvasGroup.blocksRaycasts = IsVisible;
        }
    }
}