using Bonwerk.Divvy.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class ImageElement : BackgroundElement, IContentElement
    {
        [SerializeField] private ImageStyle _style;
        [SerializeField] private Image _image;

        public RectTransform Content { get; private set; }
        
        public override BackgroundStyle BackgroundStyle => _style;

        public override Vector2 ContentSize => new Vector2(_style.Size.x, _style.Size.y);

        public Sprite Sprite
        {
            get { return _image.sprite; }
            set { _image.sprite = value; }
        }

        public float Alpha
        {
            get { return _image.color.a; }
            set
            {
                var c = _image.color;
                _image.color = new Color(c.r, c.g, c.b, value);
            }
        }

        public bool RaycastTarget
        {
            get { return _image.raycastTarget; }
            set
            {
                if (_image.raycastTarget != value) _image.raycastTarget = value;
            }
        }
        
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