using Bonwerk.Divvy.Helpers;
using Bonwerk.Divvy.Styling;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class SpriteButton : ButtonElement
    {
        [SerializeField] private Image _image;
        
        [SerializeField] private SpriteButtonStyle _style;
        public override ButtonStyle ButtonStyle => _style;

        public override Vector2 ContentSize => _style.Size;

        private RectTransform ContentTransform { get; set; }
        
        public override void Init()
        {
            base.Init();

            _image.color = _style.SpriteColor;
            _image.sprite = _style.Sprite;

            ContentTransform = _image.GetComponent<RectTransform>();
        }

        public override void SetSize(bool instant)
        {
            base.SetSize(instant);
            ContentTransform.SetPadding(_style.Padding);
        }
    }
}