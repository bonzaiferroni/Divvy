using System;
using System.Collections.Generic;
using Bonwerk.Divvy.Data;
using Bonwerk.Divvy.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Reveal
{
    public abstract class ElementRevealer
    {
        protected ElementRevealer(float time)
        {
            Time = time;
        }
        
        public float Time { get; set; }
        public bool Transitioning { get; private set; }
        public bool IsVisible { get; private set; } = true;
        public float CurrentVisibility { get; private set; } = 1;
        public float TargetVisibility { get; private set; } = 1;
        
        public event Action<bool> OnVisibilityChange;
        public event Action<bool> OnFinishedAnimation;
        
        public abstract bool InstantType { get; }
        
        protected abstract void Modify(float amount);
        
        private float _targetRef;

        public void Refresh(bool instant)
        {
            if (instant || InstantType || Mathf.Abs(CurrentVisibility - TargetVisibility) < .001f)
            {
                CurrentVisibility = TargetVisibility;
                Modify(CurrentVisibility);
                Transitioning = false;
                OnFinishedAnimation?.Invoke(IsVisible);
            }
            else
            {
                CurrentVisibility = Mathf.SmoothDamp(CurrentVisibility, TargetVisibility, ref _targetRef, Time);
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

            OnVisibilityChange?.Invoke(IsVisible);
            if (instant)
            {
                Refresh(true);
            }
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