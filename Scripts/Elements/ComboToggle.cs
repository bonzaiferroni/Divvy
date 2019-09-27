using Bonwerk.Divvy.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class ComboToggle : ToggleElement
    {
        [HideInInspector] [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private Vector2 _toggleSize = new Vector2(24, 24);
        [SerializeField] private FontStyle _fontStyle;
        [SerializeField] private ImageStyle _toggleBackgroundStyle;
        [SerializeField] private ImageStyle _toggleForegroundStyle;

        public override Vector2 ContentSize => new Vector2(_label.preferredWidth + 5 + _toggleSize.x,
            Mathf.Max(_label.preferredHeight, _toggleSize.y));
        
        protected override void Construct()
        {
            base.Construct();
            if (!_label) _label = this.GetAndValidate<TextMeshProUGUI>("Label");
        }

        protected override Toggle GetToggle()
        {
            return this.GetAndValidate<Toggle>("Content");
        }
        
        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            _toggle.targetGraphic.GetComponent<RectTransform>().sizeDelta = _toggleSize;
            AddGraphic(_label, _fontStyle);
            AddGraphic((Image) _toggle.targetGraphic, _toggleBackgroundStyle);
            AddGraphic((Image) _toggle.graphic, _toggleForegroundStyle);
        }
    }
}