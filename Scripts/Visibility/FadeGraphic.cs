using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Visibility
{
    public class FadeGraphic : AnimatedVisibility
    {
        
        public Graphic Graphic { get; private set; }
        
        public override void Init()
        {
            base.Init();
            Graphic = GetComponent<Graphic>();
        }

        protected override void Modify(float amount)
        {
            var color = Graphic.color;
            Graphic.color = new Color(color.r, color.g, color.b, amount);;
        }
    }
}