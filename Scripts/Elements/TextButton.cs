using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class TextButton : ButtonElement
    {
        [HideInInspector] [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private FontStyle _fontStyle;
        public FontStyle FontStyle => _fontStyle;

        public override Vector2 ContentSize => new Vector2(_label.preferredWidth, _label.preferredHeight);

        public string Text
        {
            get => _label.text;
            set
            {
                if (value == _label.text) return;
                _label.text = value;
                Parent?.SetLayoutDirty();
            }
        }

        protected override void Construct()
        {
            base.Construct();
            if (!_label) _label = this.GetAndValidate<TextMeshProUGUI>("Label");
            if (!_contentRect) _contentRect = _label.GetComponent<RectTransform>();
        }

        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            AddGraphic(_label, FontStyle);
        }
    }
}