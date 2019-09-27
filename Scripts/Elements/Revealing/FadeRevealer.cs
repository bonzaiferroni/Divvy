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

        protected override void Modify(float amount)
        {
            foreach (var g in Graphics)
            {
                var graphic = g.Graphic;
                var color = g.Style.Color;
                color.a *= amount;
                graphic.color = color;
                
                var raycastTarget = IsVisible && g.Style.RaycastTarget;
                if (graphic.raycastTarget != raycastTarget) graphic.raycastTarget = raycastTarget; 
            }
        }
    }
}