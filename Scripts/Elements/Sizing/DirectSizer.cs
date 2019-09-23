using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class DirectSizer : ElementSizer
    {
        private Vector2 _velocity;
        
        public DirectSizer(RectTransform transform) : base(transform)
        {
        }

        public override void Refresh(bool instant)
        {
            if (!instant && (Current - Target).sqrMagnitude > .001f)
            {
                Current = Vector2.SmoothDamp(Current, Target, ref _velocity, .2f);
                return;
            }

            FinishResize();
        }
        
        public override void FinishResize()
        {
            base.FinishResize();
            _velocity = Vector2.zero;
        }
    }
}