using Bonwerk.Divvy.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class SliderElement : BackgroundElement
    {
        [HideInInspector] [SerializeField] private Slider _slider;
        [HideInInspector] [SerializeField] private Image _sliderBackground;
        [Header("Slider")][SerializeField] private Vector2 _sliderSize = new Vector2(64, 24);
        [SerializeField] private ImageStyle _handleStyle;
        [SerializeField] private ImageStyle _fillStyle;
        [SerializeField] private ImageStyle _sliderBackgroundStyle;

        public override Vector2 ContentSize => _sliderSize;

        public override void Init()
        {
            base.Init();
            if (!_slider) _slider = this.GetAndValidate<Slider>("Content");
            if (!_sliderBackground) _sliderBackground = this.GetAndValidate<Image>("Background");
        }
        
        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            ApplyStyles.Image(_sliderBackground, _sliderBackgroundStyle);
            ApplyStyles.Image(_slider.fillRect.GetComponent<Image>(), _fillStyle);
            ApplyStyles.Image((Image) _slider.targetGraphic, _handleStyle);
            var rect = _slider.handleRect;
            rect.sizeDelta = new Vector2(_sliderSize.y, rect.sizeDelta.y);
        }
    }
}