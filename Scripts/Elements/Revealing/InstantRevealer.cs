using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class InstantRevealer : ElementRevealer
    {
        public InstantRevealer(IElement element, GraphicList graphics) : 
            base(element, 0, false)
        {
            Graphics = graphics;
        }

        private GraphicList Graphics { get; }

        public override bool InstantType => true;

        protected override float FindInitialState()
        {
            if (Graphics.Count == 0) return 0;
            return Graphics[0].Graphic.color.a == Graphics[0].Style.Color.a ? 1 : 0;
        }

        protected override void Modify(float amount)
        {
            foreach (var g in Graphics)
            {
                var graphic = g.Graphic;
                var color = g.Style.Color;
                color.a = IsVisible ? color.a : 0;
                
                graphic.color = color;
                
                var raycastTarget = IsVisible && g.Style.RaycastTarget;
                if (graphic.raycastTarget != raycastTarget) graphic.raycastTarget = raycastTarget;
            }
        }
    }
}