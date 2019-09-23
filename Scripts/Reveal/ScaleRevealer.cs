using UnityEngine;

namespace Bonwerk.Divvy.Reveal
{
    public class ScaleRevealer : ElementRevealer
    {
        public ScaleRevealer(float time, Transform transform) : base(time)
        {
            Transform = transform;
        }
        
        private Transform Transform { get; }
        
        public override bool InstantType => false;
        
        protected override void Modify(float amount)
        {
            Transform.localScale = Vector3.one * amount;
        }
    }
}