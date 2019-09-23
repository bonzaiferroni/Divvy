using Bonwerk.Divvy.Helpers;
using TMPro;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class TextButton : ButtonElement, IFontElement, IContentElement
    {
        [SerializeField] private TextButtonStyle _style;
        [SerializeField] private TextMeshProUGUI _label;
        public TextMeshProUGUI Label => _label;

        public override ElementStyle ElementStyle => _style;

        public override Vector2 ContentSize => new Vector2(_label.preferredWidth, _label.preferredHeight);
        
        public RectTransform Content { get; private set; }

        public override void Init()
        {
            base.Init();
            Content = _label.GetComponent<RectTransform>();
        }

        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            _label.fontSize = _style.FontSize;
            _label.color = _style.FontColor;
        }
    }
}