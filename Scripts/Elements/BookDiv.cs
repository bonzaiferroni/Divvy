using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class BookDiv : Div
    {
        [SerializeField] private bool _allowOff;

        private IElement _current;
        private IElement Current
        {
            get => _current;
            set => SetCurrent(value);
        }

        public delegate void BookDivListener(IElement element, bool isVisible, int index);
        public event BookDivListener OnPageChange;
        
        public override void Init()
        {
            base.Init();
            InitAllowOff();
            _current = null;
        }

        public override void AddChild(IElement child, int index = -1, bool instantPositioning = true)
        {
            base.AddChild(child, index, instantPositioning);
            child.Revealer.OnVisibilityChange += _OnVisibilityChanged;
            if (child.IsVisible)
            {
                if (Current != null && !ReferenceEquals(Current, child))
                {
                    child.Revealer.SetVisibility(false, true);
                }
                else
                {
                    Current = child;
                }
            }
        }

        public override void RemoveChild(IElement child)
        {
            base.RemoveChild(child);
            child.Revealer.OnVisibilityChange -= _OnVisibilityChanged;
            if (ReferenceEquals(Current, child)) Current = null;
            InitAllowOff();
        }

        public void ShowChild(int index)
        {
            if (index >= Children.Count) return;
            Current = Children[index];
        }

        private void _OnVisibilityChanged(IElement element, bool isVisible)
        {
            var index = -1;
            for (int i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                if (!ReferenceEquals(element, child)) continue;
                index = i;
                break;
            }

            OnPageChange?.Invoke(element, isVisible, index);

            if (isVisible)
            {
                Current = element;
            }
            else
            {
                InitAllowOff();
            }
        }

        public void InitAllowOff()
        {
            if (_allowOff || Current != null) return;
            ShowChild(0);
        }

        private void SetCurrent(IElement value)
        {
            if (_current != null && !ReferenceEquals(_current, value))
            {
                _current.Revealer.SetVisibility(false);
            }

            _current = value;
            if (_current != null && !_current.IsVisible) _current.Revealer.SetVisibility(true);
        }
    }
}