using System;
using Bonwerk.Divvy.Data;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    [Serializable]
    public class DirectPositioner : ElementPositioner
    {
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
                var nextDistance = _velocity * Time.deltaTime;
                var nextPosition = Current + nextDistance;
                var nextDelta = Target - nextPosition;
                if (Vector2.Dot(nextDelta, _velocity) < .99f )
                {
                    FinishTransport();
                }
                else
                {
                    Current = nextPosition;
                }
            }
        }
    }
}