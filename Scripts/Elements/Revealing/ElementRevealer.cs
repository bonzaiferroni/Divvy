using System;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public abstract class ElementRevealer
    {
        private float _targetRef;
        
        protected ElementRevealer(IElement element, float animationTime, bool easeAnimation)
        {
            Element = element;
            AnimationTime = animationTime;
            EaseAnimation = easeAnimation;
        }
        
        public float AnimationTime { get; set; }
        public bool EaseAnimation { get; set; }
        public bool Transitioning { get; private set; }
        public bool IsVisible { get; private set; } = true;
        public float CurrentVisibility { get; private set; } = 1;
        public float TargetVisibility { get; private set; } = 1;
        
        protected IElement Element { get; }
        
        public delegate void VisibilityListener(IElement context, bool isVisible);
        public event VisibilityListener OnVisibilityChange;
        public event VisibilityListener OnFinishedAnimation;
        
        public abstract bool InstantType { get; }

        protected abstract float FindInitialState();
        
        protected abstract void Modify(float amount);

        public void Init()
        {
            CurrentVisibility = FindInitialState();
            TargetVisibility = CurrentVisibility;
            IsVisible = CurrentVisibility > 0;
        }

        public void Refresh(bool instant)
        {
            var delta = TargetVisibility - CurrentVisibility;
            if (instant || InstantType || AnimationTime <= 0 || Mathf.Abs(delta) < .001f)
            {
                FinishReveal();
                return;
            }

            if (EaseAnimation)
            {
                CurrentVisibility = Mathf.SmoothDamp(CurrentVisibility, TargetVisibility, ref _targetRef, AnimationTime);
                Modify(CurrentVisibility);
            }
            else
            {
                var nextDistance = Time.deltaTime / AnimationTime;
                var currentDistance = Mathf.Abs(delta);
                if (nextDistance >= currentDistance)
                {
                    FinishReveal();
                }
                else
                {
                    CurrentVisibility += nextDistance * (delta / currentDistance);
                    Modify(CurrentVisibility);
                }
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
                FinishReveal();
            }

            OnVisibilityChange?.Invoke(Element, IsVisible);
        }

        protected void FinishReveal()
        {
            CurrentVisibility = TargetVisibility;
            Modify(CurrentVisibility);
            Transitioning = false;
            OnFinishedAnimation?.Invoke(Element, IsVisible);
        }

        public void SetVisibility(bool show, bool instant = false)
        {
            var target = show ? 1 : 0;
            SetVisibility(target, instant);
        }

        public void Show(bool instant = false)
        {
            SetVisibility(1, instant);
        }

        public void Hide(bool instant = false)
        {
            SetVisibility(0, instant);
        }

        public void Toggle(bool instant = false)
        {
            SetVisibility(!IsVisible, instant);
        }
    }
}