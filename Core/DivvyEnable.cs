using UnityEngine.UI;

namespace Divvy.Core
{
    public class DivvyEnable : DivvyInstantVisibility
    {
        private Graphic _graphic;

        public Graphic Graphic
        {
            get
            {
                if (!_graphic) _graphic = GetComponent<Graphic>();
                return _graphic;
            }
        }

        protected override void Modify(bool isVisible)
        {
            Graphic.enabled = isVisible;
        }
    }
}