using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class InstantSizer : ElementSizer
    {
        public InstantSizer(RectTransform transform) : base(transform, 0)
        {
        }

        public override void Refresh(bool instant)
        {
            FinishResize();
        }
    }
}