using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Core
{
    [RequireComponent(typeof(Image))]
    public class DivSprite : Element
    {
        private Image _image;

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

        internal override void Init()
        {
            _image = GetComponent<Image>(); // needs to come before base.Init()
            _image.preserveAspect = true;
            base.Init();
        }
    }
}