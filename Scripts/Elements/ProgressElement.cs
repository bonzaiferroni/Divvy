using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public abstract class ProgressElement : BackgroundElement
    {
        [HideInInspector] [SerializeField] protected Image _progressBackground;
        [HideInInspector] [SerializeField] protected Image _progress;
        [HideInInspector] [SerializeField] protected RectTransform _progressRect;
        [Header("Progress")] [SerializeField] private ImageStyle _progressBackgroundStyle;
        [SerializeField] private ImageStyle _progressStyle;
        [SerializeField] [Range(0, 1)]private float _initialFill;
        [SerializeField] private bool _forwardProgress;
        [SerializeField] private Vector2 _contentSize;
        private float _fill;
        private bool _reportedFull;
        public override Vector2 ContentSize => _contentSize;
        private AnimateFloat FillAnimator { get; set; }

        protected abstract void DisplayFill(float value);

        public delegate void ProgressListener();
        public event ProgressListener OnFullProgress;

        public float Fill
        {
            get => _fill;
            set => SetFill(value);
        }

        protected override void Construct()
        {
            base.Construct();
            if (!_progressBackground) _progressBackground = this.GetAndValidate<Image>("Background");
            if (!_progress) _progress = this.GetAndValidate<Image>("Progress");
            if (!_progressRect) _progressRect = _progress.GetComponent<RectTransform>();
            FillAnimator = new AnimateFloat(_initialFill, ElementStyle.AnimationTime, ElementStyle.EaseAnimation);
        }

        protected override void Associate()
        {
            base.Associate();
            AddGraphic(_progressBackground, _progressBackgroundStyle);
            AddGraphic(_progress, _progressStyle);
            FillAnimator.OnModify += _DisplayFill;
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
            value = Mathf.Clamp01(value);
            if (_forwardProgress && value < _fill)
            {
                _fill = value;
                value += 1;
            }
            else
            {
                _fill = value;
            }

            if (FillAnimator.Current > 1) FillAnimator.SetValue(FillAnimator.Current - 1, true);
            FillAnimator.SetValue(value, false);
        }

        private void _DisplayFill(float value)
        {
            if (value >= 1)
            {
                if (!_reportedFull)
                {
                    _reportedFull = true;
                    OnFullProgress?.Invoke();
                }
            }
            else
            {
                if (_reportedFull)
                {
                    _reportedFull = false;
                }
            }
            
            if (value > 1) value -= 1;
            DisplayFill(value);
        }
    }
}