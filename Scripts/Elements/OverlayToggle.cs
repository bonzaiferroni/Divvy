using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public abstract class OverlayToggle : ToggleElement
    {
        [SerializeField] private ImageStyle _overlayStyle = new ImageStyle(new Color(1, 1, 1, .2f));

        protected override Toggle GetToggle()
        {
            return GetComponent<Toggle>();
        }

        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            ApplyStyles.Image((Image) _toggle.graphic, _overlayStyle);
        }
    }
}