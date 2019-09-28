using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class ImageElement : BackgroundElement
    {
        [HideInInspector] [SerializeField] private Image _image;
        [Header("Image")] [SerializeField] private Vector2 _contentSize = new Vector2(128, 128);
        public override Vector2 ContentSize => _contentSize;
        [SerializeField] private ImageStyle _style = new ImageStyle(Color.white);

        public Sprite Sprite
        {
            get => _image.sprite;
            set => _image.sprite = value;
        }

        public bool RaycastTarget
        {
            get { return _image.raycastTarget; }
            set
            {
                if (_image.raycastTarget != value) _image.raycastTarget = value;
            }
        }
        
        protected override void Construct()
        {
            base.Construct();
            if (!_image) _image = this.GetAndValidate<Image>("Sprite");
            if (!_contentRect) _contentRect = _image.GetComponent<RectTransform>();
        }

        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            AddGraphic(_image, _style);
        }
    }
}