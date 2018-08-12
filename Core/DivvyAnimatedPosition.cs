using System;
using UnityEngine;

namespace Divvy.Core
{
    public class DivvyAnimatedPosition : DivvyPosition
    {
        private Vector2 _posRef;
        
        public override void TransportSelf(bool instant)
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
            _posRef = Target;
        }
    }
}