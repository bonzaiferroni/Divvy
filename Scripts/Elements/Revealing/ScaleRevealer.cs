using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class ScaleRevealer : ElementRevealer
    {
        public ScaleRevealer(IElement element, Transform transform, float animationTime, bool easeAnimation) : 
            base(element, animationTime, easeAnimation)
        {
            Transform = transform;
        }

        private Transform Transform { get; }

        public override bool InstantType => false;

        protected override float FindInitialState()
        {
            return Transform.localScale.x / 1;
        }

        protected override void Modify(float amount)
        {
            Transform.localScale = Vector3.one * amount;
        }
    }
}