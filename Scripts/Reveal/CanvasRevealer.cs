using UnityEngine;

namespace Bonwerk.Divvy.Reveal
{
    public class CanvasRevealer : ElementRevealer
    {
        public CanvasRevealer(float time, CanvasGroup canvasGroup) : base(time)
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