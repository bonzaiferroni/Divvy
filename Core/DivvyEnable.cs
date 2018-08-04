using UnityEngine;
using UnityEngine.UI;

namespace Divvy.Core
{
    public class DivvyEnable : DivvyInstantVisibility
    {
        [SerializeField] private Graphic _graphic;

        public Graphic Graphic => _graphic;

        protected override void Modify(bool isVisible)
        {
            Graphic.enabled = isVisible;
        }
    }
}