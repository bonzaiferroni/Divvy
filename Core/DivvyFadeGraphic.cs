using UnityEngine;
using UnityEngine.UI;

namespace Divvy.Core
{
    public class DivvyFadeGraphic : DivvyAnimatedVisibility
    {
        
        public Graphic Graphic { get; private set; }
        
        public override void Init()
        {
            base.Init();
            Graphic = GetComponent<Graphic>();
        }

        protected override void Modify(float amount)
        {
            Graphic.color = new Color(Graphic.color.r, Graphic.color.g, Graphic.color.b, amount);
        }
    }
}