using System;
using System.Collections;
using UnityEngine;

namespace Divvy.Core
{
    [ExecuteInEditMode]
    public abstract class DivvyVisibility : MonoBehaviour
    {
        [SerializeField] private bool _isVisible = true;
        
        protected bool _initialized;
        
        public event Action<bool> OnVisibilityChange;

        public abstract void Show();
        public abstract void Hide();
        public abstract void Toggle();
        public abstract void SetVisibility(bool isVisible);
        
        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }

        private void Start()
        {
            // if (!_initialized) Init();
        }

        public virtual void Init()
        {
            SetVisibility(IsVisible);
            _initialized = true;
        }

        protected void VisibilityChangeHandler(bool isVisible)
        {
            OnVisibilityChange?.Invoke(isVisible);
        }
    }
}