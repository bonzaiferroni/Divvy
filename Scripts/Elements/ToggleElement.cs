using Bonwerk.Divvy.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class ToggleElement : BackgroundElement
    {
        [HideInInspector] [SerializeField] private TextMeshProUGUI _label;
        [HideInInspector] [SerializeField] private Toggle _toggle;
        [Header("Toggle") ][SerializeField] private Vector2 _toggleSize = new Vector2(24, 24);
        [SerializeField] private FontStyle _fontStyle;
        [SerializeField] private ImageStyle _toggleBackgroundStyle;
        [SerializeField] private ImageStyle _toggleForegroundStyle;
        [SerializeField] private SelectableStyle _selectableStyle;

        public override Vector2 ContentSize => new Vector2(_label.preferredWidth + 5 + _toggleSize.x,
            Mathf.Max(_label.preferredHeight, _toggleSize.y));
        
        public override void Init()
        {
            base.Init();
            if (!_label) _label = this.GetAndValidate<TextMeshProUGUI>("Label");
            if (!_toggle) _toggle = this.GetAndValidate<Toggle>("Content");
        }
        
        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            _toggle.targetGraphic.GetComponent<RectTransform>().sizeDelta = _toggleSize;
            ApplyStyles.Font(_label, _fontStyle);
            ApplyStyles.Image((Image) _toggle.targetGraphic, _toggleBackgroundStyle);
            ApplyStyles.Image((Image) _toggle.graphic, _toggleForegroundStyle);
            ApplyStyles.Selectable(_toggle, _background, _selectableStyle);
        }
    }
}