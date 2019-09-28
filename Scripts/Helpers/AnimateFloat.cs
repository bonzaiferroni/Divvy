using UnityEngine;

namespace Bonwerk.Divvy
{
    public class AnimateFloat
    {
        private float _velocity;
        private float _current;
        
        public AnimateFloat(float initial, float animationTime, bool easeAnimation)
        {
            _current = initial;
            Target = initial;
            AnimationTime = animationTime;
            EaseAnimation = easeAnimation;
        }
        public float Target { get; private set; }
        public float AnimationTime { get; set; }
        public bool EaseAnimation { get; set; }
        public bool Animating { get; private set; }

        public delegate void FloatListener(float value);
        public event FloatListener OnFinish;
        public event FloatListener OnModify;

        public float Current
        {
            get => _current;
            private set
            {
                _current = value;
                OnModify?.Invoke(value);
            }
        }

        public void Refresh(bool instant)
        {
            var delta = Target - Current;
            if (instant || AnimationTime <= 0 || Mathf.Abs(delta) < .001f)
            {
                FinishAnimating();
                return;
            }

            if (EaseAnimation)
            {
                Current = Mathf.SmoothDamp(Current, Target, ref _velocity, AnimationTime);
            }
            else
            {
                var nextDistance = _velocity * Time.deltaTime;
                if ((nextDistance < 0 && delta > nextDistance) || (nextDistance > 0 && delta < nextDistance))
                {
                    FinishAnimating();
                }
                else
                {
                    Current += nextDistance;
                }
            }
        }

        public void SetValue(float value, bool instant)
        {
            if (value == Target) return;
            Target = value;
            Animating = true;

            if (instant)
            {
                FinishAnimating();
            }
            else if (!EaseAnimation && AnimationTime > 0)
            {
                _velocity = (Target - Current) / AnimationTime;
            }
        }

        private void FinishAnimating()
        {
            Current = Target;
            Animating = false;
            _velocity = 0;
            OnFinish?.Invoke(Current);
        }
    }
}