using System;
using UnityEngine;

namespace Bonwerk.Divvy.Positioning
{
    [Serializable]
    public class DivAnimatedPosition : DivPosition
    {
        private Vector2 _posRef;
        
        public DivAnimatedPosition(RectTransform rect) : base(rect)
        {
        }
        
        public override void Refresh(bool instant)
        {
            if (Transported) return;

            if (!instant && (Current - Target).sqrMagnitude > .001f)
            {
                Current = Vector2.SmoothDamp(Current, Target, ref _posRef, .2f);
                return;
            }

            FinishTransport();
        }

        public override void FinishTransport()
        {
            base.FinishTransport();
            _posRef = Vector2.zero;
        }
    }
}