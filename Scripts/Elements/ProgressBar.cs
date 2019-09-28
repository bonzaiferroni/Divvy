using Bonwerk.Divvy.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class ProgressBar : BackgroundElement
    {
        [HideInInspector] [SerializeField] private Image _barBackground;
        [HideInInspector] [SerializeField] private Image _bar;
        [HideInInspector] [SerializeField] private RectTransform _barRect;
        [Header("Progress Bar")] [SerializeField] private ImageStyle _barBackgroundStyle;
        [SerializeField] private ImageStyle _barStyle;
        [SerializeField] [Range(0, 1)]private float _initialFill;
        [SerializeField] private Vector2 _contentSize;
        public override Vector2 ContentSize => _contentSize;
        private AnimateFloat FillAnimator { get; set; }

        public float Fill
        {
            get => FillAnimator.Target;
            set
            {
                value = Mathf.Clamp01(value);
                FillAnimator.SetValue(value, false);
            }
        }

        protected override void Construct()
        {
            base.Construct();
            if (!_barBackground) _barBackground = this.GetAndValidate<Image>("Background");
            if (!_bar) _bar = this.GetAndValidate<Image>("Bar");
            if (!_barRect) _barRect = _bar.GetComponent<RectTransform>();
            FillAnimator = new AnimateFloat(_initialFill, .2f, false);
        }

        protected override void Associate()
        {
            base.Associate();
            AddGraphic(_barBackground, _barBackgroundStyle);
            AddGraphic(_bar, _barStyle);
            FillAnimator.OnModify += SetFill;
            SetFill(_initialFill);
            if (Application.isPlaying)
            {
                InvokeRepeating(nameof(TestAnimation), 2, 2);
            }
        }

        private void TestAnimation()
        {
            Fill = Random.value;
        }

        public override void Refresh(bool instant)
        {
            base.Refresh(instant);
            if (FillAnimator.Animating) FillAnimator.Refresh(instant);
        }

        private void SetFill(float value)
        {
            _barRect.sizeDelta = new Vector2(_contentSize.x * value, _contentSize.y);
        }
    }
}