using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class PageDiv : Div
    {
        public delegate void BookDivListener(IElement element, bool isVisible, int index);
        public event BookDivListener OnPageChange;
        
        protected override void Construct()
        {
            base.Construct();
            OnPageChange = null;
        }

        public override void AddChild(IElement child, int index = -1, bool instantPositioning = true)
        {
            base.AddChild(child, index, instantPositioning);
            child.Revealer.OnVisibilityChange += _OnVisibilityChanged;
        }

        public override void RemoveChild(IElement child)
        {
            base.RemoveChild(child);
            child.Revealer.OnVisibilityChange -= _OnVisibilityChanged;
        }

        public void ShowPage(int index, bool show, bool instant = false)
        {
            if (index >= Children.Count) return;
            Children[index].Revealer.SetVisibility(show, instant);
        }

        private void _OnVisibilityChanged(IElement element, bool isVisible)
        {
            var index = Children.IndexOf(element);
            OnPageChange?.Invoke(element, isVisible, index);
        }
    }
}