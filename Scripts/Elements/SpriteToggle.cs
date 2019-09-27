using Bonwerk.Divvy.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class SpriteToggle : OverlayToggle
    {
        [HideInInspector][SerializeField] private Image _image;
        [SerializeField] private ImageStyle _spriteStyle;
        [SerializeField] private Vector2 _contentSize = new Vector2(32, 32);
        public override Vector2 ContentSize => _contentSize;
        
        protected override void Construct()
        {
            base.Construct();
            if (!_image) _image = this.GetAndValidate<Image>("Sprite");
            if (!_contentRect) _contentRect = _image.GetComponent<RectTransform>();
        }

        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            AddGraphic(_image, _spriteStyle);
        }
    }
}