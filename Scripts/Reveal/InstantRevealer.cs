using UnityEngine;

namespace Bonwerk.Divvy.Reveal
{
    public class InstantRevealer : ElementRevealer
    {
        public InstantRevealer(RectTransform transform) : base(0)
        {
            Transform = transform;
        }
        
        private RectTransform Transform { get; }

        private bool InPosition { get; set; } = true;
        
        public override bool InstantType => true;

        protected override void Modify(float amount)
        {
            if (InPosition && !IsVisible)
            {
                Transform.anchoredPosition *= Vector3.up * 10000;
                InPosition = false;
            }
            else if (!InPosition && IsVisible)
            {
                InPosition = true;
            }
        }
    }
}