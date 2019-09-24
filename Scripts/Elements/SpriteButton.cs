using Bonwerk.Divvy.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class SpriteButton : ButtonElement
    {
        [Header("SpriteButton")]
        [HideInInspector][SerializeField] private Image _image;
        [SerializeField] private ImageStyle _style;
        [SerializeField] private Vector2 _contentSize = new Vector2(32, 32);
        public override Vector2 ContentSize => _contentSize;
        
        public override void Init()
        {
            base.Init();
            if (!_image) _image = transform.GetChild(0).GetComponent<Image>();
            if (!_contentRect) _contentRect = _image.GetComponent<RectTransform>();
        }

        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            ApplyStyles.Image(_image, _style);
        }
    }
}