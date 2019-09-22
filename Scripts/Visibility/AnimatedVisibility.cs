using System;
using Bonwerk.Divvy.Data;
using UnityEngine;

namespace Bonwerk.Divvy.Visibility
{
    public abstract class AnimatedVisibility : ElementVisibility
    {
        [SerializeField] private float _speed = DivConstants.Speed;

        private float _targetRef;

        public bool Transitioning { get; private set; }
        public float CurrentVisibility { get; private set; }
        public float TargetVisibility { get; private set; }

        public event Action<bool> OnFinishedAnimation;

        public override void Init()
        {
            SetVisibility(IsVisible, true);
        }

        private void Update()
        {
            ModifyVisibility();
        }

        private void ModifyVisibility()
        {
            if (!Transitioning) return;

            if (Mathf.Abs(CurrentVisibility - TargetVisibility) < .001f)
            {
                CurrentVisibility = TargetVisibility;
                Modify(CurrentVisibility);
                Transitioning = false;
                OnFinishedAnimation?.Invoke(IsVisible);
            }
            else
            {
                CurrentVisibility = Mathf.SmoothDamp(CurrentVisibility, TargetVisibility, ref _targetRef, _speed);
                Modify(CurrentVisibility);
            }
        }

        public void SetVisibility(float target, bool instant = false)
        {
            if (float.IsNaN(target)) target = 0;
            target = Mathf.Clamp(target, 0, 1);

            if (target == TargetVisibility) return;
            
            IsVisible = target > 0;
            TargetVisibility = target;
            Transitioning = true;

            if (instant)
            {
                CurrentVisibility = target;
                ModifyVisibility();
            }

            VisibilityChangeHandler(IsVisible);
        }

        public override void SetVisibility(bool show, bool instant = false)
        {
            var target = show ? 1 : 0;
            SetVisibility(target, instant);
        }

        public override void Show()
        {
            Show(false);
        }

        public void Show(bool instant)
        {
            SetVisibility(1, instant);
        }

        public override void Hide()
        {
            Hide(false);
        }

        public void Hide(bool instant)
        {
            SetVisibility(0, instant);
        }

        public override void Toggle()
        {
            Toggle(false);
        }

        public void Toggle(bool instant)
        {
            SetVisibility(!IsVisible, instant);
        }

        protected abstract void Modify(float amount);
    }
}