﻿namespace Bonwerk.Divvy.Core
{
    public abstract class DivInstantVisibility : DivVisibility
    {
        public override void Show()
        {
            SetVisibility(true);
        }

        public override void Hide()
        {
            SetVisibility(false);
        }

        public override void Toggle()
        {
            SetVisibility(!IsVisible);
        }

        public override void SetVisibility(bool isVisible, bool instant = false)
        {
            IsVisible = isVisible;
            Modify(isVisible);
            VisibilityChangeHandler(isVisible);
        }

        protected abstract void Modify(bool isVisible);
    }
}