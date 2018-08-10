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

            if (!instant)
            {
                if (Math.Abs(Current.x - Target.x) > .001f)
                {
                    var x = Mathf.SmoothDamp(Current.x, Target.x, ref _posRef.x, .2f);
                    Current = new Vector2(x, Current.y);
                    return;
                }
			
                if (Math.Abs(Current.y - Target.y) > .001f)
                {
                    var y = Mathf.SmoothDamp(Current.y, Target.y, ref _posRef.y, .2f);
                    Current = new Vector2(Current.x, y);
                    return;
                }
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