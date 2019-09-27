using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class InstantPositioner : ElementPositioner
    {
        public InstantPositioner(RectTransform transform) : base(transform, 0)
        {
        }

        public override void Refresh(bool instant)
        {
            FinishTransport();
        }
    }
}