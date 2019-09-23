using System;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    [Serializable]
    public class DirectPositioner : ElementPositioner
    {
        private Vector2 _velocity;
        
        public DirectPositioner(RectTransform transform, float animationTime) : base(transform, animationTime)
        {
        }
        
        public override void Refresh(bool instant)
        {
            var delta = Target - Current;
            var squaredMagnitude = delta.sqrMagnitude;
            if (instant || AnimationTime <= 0 || squaredMagnitude < .001f)
            {
                FinishTransport();
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
                    FinishTransport();
                }
                else
                {
                    Current += delta.normalized * nextDistance;
                }
            }
        }

        public override void FinishTransport()
        {
            base.FinishTransport();
            _velocity = Vector2.zero;
        }
    }
}