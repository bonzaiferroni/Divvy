﻿using System;
using UnityEngine;

namespace Bonwerk.Divvy.Core
{
    [ExecuteInEditMode]
    public abstract class DivVisibility : MonoBehaviour
    {
        [SerializeField] private bool _isVisible = true;
        
        public event Action<bool> OnVisibilityChange;

        public abstract void Init();
        public abstract void Show();
        public abstract void Hide();
        public abstract void Toggle();
        public abstract void SetVisibility(bool isVisible, bool instant = false);
        
        public bool IsVisible
        {
            get { return _isVisible; }
            protected set { _isVisible = value; }
        }

        protected void VisibilityChangeHandler(bool isVisible)
        {
            OnVisibilityChange?.Invoke(isVisible);
        }
    }
}