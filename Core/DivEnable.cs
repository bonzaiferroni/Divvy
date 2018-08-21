using UnityEngine;
using UnityEngine.UI;

namespace DivLib.Core
{
    public class DivEnable : DivInstantVisibility
    {
        [SerializeField] private Graphic _graphic;

        public Graphic Graphic
        {
            get { return _graphic; }
            set { _graphic = value; }
        }

        protected override void Modify(bool isVisible)
        {
            Graphic.enabled = isVisible;
        }
    }
}