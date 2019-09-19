using Bonwerk.Divvy.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    [RequireComponent(typeof(Image))]
    public class DivSprite : Element
    {
        [SerializeField] private Vector2 _contentSize;
        
        private Image _image;
        
        public override bool Expand { get; }
        public override Spacing Margin { get; }
        public override Spacing Padding { get; }

        public override Vector2 ContentSize => _contentSize;

        public Sprite Sprite
        {
            get { return _image.sprite; }
            set { _image.sprite = value; }
        }

        public float Alpha
        {
            get { return _image.color.a; }
            set { _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, value); }
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
            _image = GetComponent<Image>();
            _image.preserveAspect = true;
        }
    }
}