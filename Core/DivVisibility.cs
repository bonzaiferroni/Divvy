using System;
using UnityEngine;

namespace DivLib.Core
{
    [ExecuteInEditMode]
    public abstract class DivVisibility : MonoBehaviour
    {
        [SerializeField] private bool _isVisible = true;
        
        protected bool Initialized;
        
        public event Action<bool> OnVisibilityChange;

        public abstract void Show();
        public abstract void Hide();
        public abstract void Toggle();
        public abstract void SetVisibility(bool isVisible, bool instant = false);
        
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
            Initialized = true;
        }

        protected void VisibilityChangeHandler(bool isVisible)
        {
            OnVisibilityChange?.Invoke(isVisible);
        }
    }
}