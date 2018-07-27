using System;
using System.Collections;
using UnityEngine;

namespace Divvy.Core
{
    [ExecuteInEditMode]
    public abstract class DivvyVisibility : MonoBehaviour
    {
        public event Action<bool> OnFinishedAnimation;
        public event Action<bool> OnVisibilityChange;

        [SerializeField] private bool _isVisible = true;
        
        private float _targetRef;
        private bool _initialized;

        public bool Transitioning { get; private set; }
        public float CurrentVisibility { get; private set; }
        public float TargetVisibility { get; private set; }

        public void Construct(bool isVisible)
        {
            _isVisible = isVisible;
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }

        private void Start()
        {
            if (!_initialized) Init();
        }

        public virtual void Init()
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

            if (Mathf.Abs(CurrentVisibility - TargetVisibility) < .001f)
            {
                OnFinishedAnimation?.Invoke(IsVisible);
                CurrentVisibility = TargetVisibility;
                Modify(CurrentVisibility);
                Transitioning = false;
            }
            else
            {
                CurrentVisibility = Mathf.SmoothDamp(CurrentVisibility, TargetVisibility, ref _targetRef, DivvyConstants.Speed);
                Modify(CurrentVisibility);
            }
        }

        public void SetVisibility(bool show, bool instant = false)
        {
            var target = show ? 1 : 0;
            SetVisibility(target, instant);
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
            }
            
            OnVisibilityChange?.Invoke(IsVisible);
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

        protected abstract void Modify(float amount);
    }
}