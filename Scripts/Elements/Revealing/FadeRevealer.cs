using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class FadeRevealer : ElementRevealer
    {
        public FadeRevealer(Graphic[] graphics, float animationTime, bool easeAnimation) : base(animationTime,
            easeAnimation)
        {
            Graphics = graphics;
            InitialValues = new float[graphics.Length];
            for (int i = 0; i < graphics.Length; i++)
            {
                InitialValues[i] = graphics[i].color.a;
            }
        }

        private float[] InitialValues { get; }

        private Graphic[] Graphics { get; }

        public override bool InstantType => false;

        protected override void Modify(float amount)
        {
            for (int i = 0; i < Graphics.Length; i++)
            {
                var graphic = Graphics[i];
                var initialValue = InitialValues[i];
                var color = graphic.color;
                graphic.color = new Color(color.r, color.g, color.b, amount * initialValue);
                if (graphic.raycastTarget != IsVisible) graphic.raycastTarget = IsVisible;
            }
        }
    }
}