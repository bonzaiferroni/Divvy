using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class FadeRevealer : ElementRevealer
    {
        public FadeRevealer(IElement element, GraphicList graphics, float animationTime, bool easeAnimation) :
            base(element, animationTime, easeAnimation)
        {
            Graphics = graphics;
        }

        private GraphicList Graphics { get; }

        public override bool InstantType => false;

        protected override float FindInitialState()
        {
            if (Graphics.Count == 0) return 0;
            return Graphics[0].Graphic.color.a / Graphics[0].Style.Color.a;
        }

        protected override void Modify(float amount)
        {
            foreach (var g in Graphics)
            {
                var graphic = g.Graphic;
                graphic.ChangeAlphaOnly(amount);
                
                var raycastTarget = IsVisible && g.Style.RaycastTarget;
                if (graphic.raycastTarget != raycastTarget) graphic.raycastTarget = raycastTarget; 
            }
        }
    }
}