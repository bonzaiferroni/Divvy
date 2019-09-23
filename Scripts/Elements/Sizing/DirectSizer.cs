using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class DirectSizer : ElementSizer
    {
        private Vector2 _velocity;
        
        public DirectSizer(RectTransform transform, float animationTime) : base(transform, animationTime)
        {
        }

        public override void Refresh(bool instant)
        {
            var delta = Target - Current;
            var squaredMagnitude = delta.sqrMagnitude;
            if (instant || AnimationTime <= 0 || squaredMagnitude < .001f)
            {
                FinishResize();
            }

            if (EaseAnimation)
            {
                Current = Vector2.SmoothDamp(Current, Target, ref _velocity, AnimationTime);
            }
            else
            {
                var nextDistance = Time.deltaTime / AnimationTime * 30;
                if (nextDistance * nextDistance >= squaredMagnitude)
                {
                    FinishResize();
                }
                else
                {
                    Current += delta.normalized * nextDistance;
                }
            }
        }
        
        public override void FinishResize()
        {
            base.FinishResize();
            _velocity = Vector2.zero;
        }
    }
}