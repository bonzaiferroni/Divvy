using Bonwerk.Divvy.Helpers;
using Bonwerk.Divvy.Styling;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class ImageElement : Element
    {
        [SerializeField] private Image _image;
        [SerializeField] private ImageStyle _style;

        public override ElementStyle ElementStyle => _style;

        public override Vector2 ContentSize => new Vector2(_style.Size.x, _style.Size.y);

        private RectTransform ImageTransform { get; set; }
        private Image BackgroundImage { get; set; }
        
        public override void Init()
        {
            base.Init();
            
            _image.color = _style.SpriteColor;
            _image.sprite = _style.Sprite;

            ImageTransform = _image.GetComponent<RectTransform>();
            BackgroundImage = GetComponent<Image>();

            if (BackgroundImage)
            {
                BackgroundImage.color = _style.BackgroundColor;
                BackgroundImage.sprite = _style.BackgroundSprite;
            }
        }


        public override void SetSize(bool instant)
        {
            base.SetSize(instant);
            ImageTransform.SetPadding(_style.Padding);
        }
    }
}