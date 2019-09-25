using Bonwerk.Divvy.Helpers;
using TMPro;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class TextToggle : OverlayToggle
    {
        [HideInInspector] [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private FontStyle _fontStyle;
        public FontStyle FontStyle => _fontStyle;

        public override Vector2 ContentSize => new Vector2(_label.preferredWidth, _label.preferredHeight);

        public override void Init()
        {
            base.Init();
            if (!_label) _label = this.GetAndValidate<TextMeshProUGUI>("Label");
            if (!_contentRect) _contentRect = _label.GetComponent<RectTransform>();
        }

        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            ApplyStyles.Font(_label, FontStyle);
        }
    }
}