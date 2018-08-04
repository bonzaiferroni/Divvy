using System;
using UnityEngine;

namespace Divvy.Core
{
    public abstract class DivvyAnimatedVisibility : DivvyVisibility
    {
        private float _targetRef;

        public bool Transitioning { get; private set; }
        public float CurrentVisibility { get; private set; }
        public float TargetVisibility { get; private set; }

        public event Action<bool> OnFinishedAnimation;
        
        public override void Init()
        {
            SetVisibility(IsVisible, true);
            _initialized = true;
        }
        
        private void Update()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                SetVisibility(IsVisible, true);
            }
#endif
            ModifyVisibility();
        }

        private void ModifyVisibility()
        {
            if (!Transitioning) return;

            if (name == "CodexCursor") Debug.Log($"{Mathf.Abs(CurrentVisibility - TargetVisibility) < .001f}");
            
            if (Mathf.Abs(CurrentVisibility - TargetVisibility) < .001f)
            {
                CurrentVisibility = TargetVisibility;
                Modify(CurrentVisibility);
                Transitioning = false;
                OnFinishedAnimation?.Invoke(IsVisible);
            }
            else
            {
                CurrentVisibility = Mathf.SmoothDamp(CurrentVisibility, TargetVisibility, ref _targetRef, DivvyConstants.Speed);
                Modify(CurrentVisibility);
            }
        }

        public void SetVisibility(float target, bool instant = false)
        {
            if (target == TargetVisibility)
                return;
            
            if (float.IsNaN(target)) target = 0;
            target = Mathf.Clamp(target, 0, 1);

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

        public override void SetVisibility(bool isVisible)
        {
            SetVisibility(isVisible, false);
        }
        
        public void SetVisibility(bool show, bool instant)
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