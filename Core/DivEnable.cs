using Divvy.Core;
using UnityEngine;
using UnityEngine.UI;

namespace DivLib.Core
{
    public class DivEnable : DivInstantVisibility
    {
        [SerializeField] private Graphic _graphic;

        public Graphic Graphic => _graphic;

        protected override void Modify(bool isVisible)
        {
            Graphic.enabled = isVisible;
        }
    }
}