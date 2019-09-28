using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class CanvasRevealer: ElementRevealer
    {
        public CanvasRevealer(IElement element, Canvas canvas) : base(element, 0, false)
        {
            Canvas = canvas;
        }

        private Canvas Canvas { get; }

        public override bool InstantType => true;

        protected override float FindInitialState()
        {
            return Canvas.enabled ? 1 : 0;
        }

        protected override void Modify(float amount)
        {
            Canvas.enabled = IsVisible;
        }
    }
}