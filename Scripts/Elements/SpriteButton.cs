using Bonwerk.Divvy.Helpers;
using Bonwerk.Divvy.Styling;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class SpriteButton : ButtonElement, IContentElement
    {
        [SerializeField] private SpriteButtonStyle _style;
        [SerializeField] private Image _image;
        
        public override ElementStyle ElementStyle => _style;

        public override Vector2 ContentSize => _style.Size;

        public RectTransform Content { get; private set; }
        
        public override void Init()
        {
            base.Init();
            Content = _image.GetComponent<RectTransform>();
        }

        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            _image.color = _style.SpriteColor;
            _image.sprite = _style.Sprite;
        }
    }
}