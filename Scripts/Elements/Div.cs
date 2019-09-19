using System;
using System.Collections.Generic;
using Bonwerk.Divvy.Data;
using Bonwerk.Divvy.Styling;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    [ExecuteInEditMode]
    public class Div : Element
    {
        [SerializeField] private DivStyle _style;
        [SerializeField] private bool _reverseOrder;

        private Vector2 _contentSize;

        private readonly Stack<IElement> _newChildren = new Stack<IElement>();
        private readonly Stack<IElement> _expand = new Stack<IElement>();

        public List<IElement> Children { get; } = new List<IElement>();

        public bool IsDirty { get; set; } = true;

        public Image BackgroundImage { get; private set; }

        public override Vector2 ContentSize => _contentSize;

        public override Spacing Margin => _style.Margin;

        public override Spacing Padding => _style.Padding;

        public override bool Expand => _style.Expand;

        public Vector2 MinSize => _style.MinSize;

        public Vector2 ChildOrientation => _style.ChildOrientation;

        public float Spacing => _style.Spacing;

        public bool ExpandChildren => _style.ExpandChildren;

        public LayoutType Layout => _style.Layout;

        public bool ReverseOrder
        {
            get { return _reverseOrder; }
            set
            {
                if (_reverseOrder == value) return;
                _reverseOrder = value;
                IsDirty = true;
            }
        }

        // life cycle  

        public override void Init()
        {
            base.Init();
            FindChildren();
            BackgroundImage = GetComponent<Image>();
        }

        private void FindChildren()
        {
            Children.Clear();
            _newChildren.Clear();
            for (var i = 0; i < transform.childCount; i++)
            {
                var childTransform = transform.GetChild(i);
                var child = childTransform.GetComponent<IElement>();
                if (child == null) continue;
                child.Init();
                AddChild(child);
            }

            IsDirty = true;
        }

        public override void Refresh(bool instant)
        {
            if (!IsDirty) return;
            IsDirty = false;

            foreach (var child in Children)
            {
                child.Refresh(instant);
            }

            PositionChildren(instant);
            base.Refresh(instant);
        }

        // public

        public void AddAfter(IElement child, IElement afterChild)
        {
            var index = Children.IndexOf(afterChild);
            if (index < 0) throw new Exception("Didn't find child to insert after");
            AddChild(child, index + 1);
        }

        public void AddChild(IElement child, int index = -1, bool instantPositioning = true)
        {
            if (child.Parent) child.Parent.RemoveChild(child);
            if (index >= 0)
            {
                Children.Insert(index, child);
            }
            else
            {
                Children.Add(child);
            }

            child.Parent = this;
            if (child.Transform != transform) child.Transform.SetParent(transform, false);
            if (instantPositioning) _newChildren.Push(child);
            IsDirty = true;
        }

        public void RemoveChild(IElement child)
        {
            if (child.Parent != this) throw new Exception("Cannot remove child with a different parent");
            child.Parent = null;
            IsDirty = true;
            Children.Remove(child);
        }

        public IElement GetChild(string objectTag)
        {
            foreach (var child in Children)
            {
                if (child.Name == objectTag) return child;
            }

            foreach (var child in Children)
            {
                var div = child as Div;
                if (div == null) continue;
                var grandChild = div.GetChild(objectTag);
                if (grandChild == null) continue;
                return grandChild;
            }

            return null;
        }

        public T GetChild<T>(string objectTag) where T : Element
        {
            foreach (var child in Children)
            {
                if (child.Name == objectTag && child is T) return child as T;
            }

            foreach (var child in Children)
            {
                var div = child as Div;
                if (div == null) continue;
                var grandChild = div.GetChild<T>(objectTag);
                if (grandChild == null) continue;
                return grandChild;
            }

            return null;
        }

        // Position Children

        private void PositionChildren(bool instant)
        {
            SetPositions(instant);
            FinishNewChildren();
        }

        private void SetPositions(bool instant)
        {
            var direction = Layout == LayoutType.Horizontal ? Vector2.right : Vector2.down;
            var childOrientation = Layout == LayoutType.Horizontal
                ? new Vector2(0, ChildOrientation.y)
                : new Vector2(ChildOrientation.x, 1);

            var paddingX = (1 - childOrientation.x) * Padding.Left + childOrientation.x * -Padding.Right;
            var paddingY = (1 - childOrientation.y) * Padding.Bottom + childOrientation.y * -Padding.Top;

            var paddingPosition = new Vector2(paddingX, paddingY);
            var position = paddingPosition;
            var maxSize = Vector2.zero;

            for (int i = 0; i < Children.Count; i++)
            {
                var index = ReverseOrder ? Children.Count - i - 1 : i;
                var child = Children[index];
                if (!child.IsVisible) continue;
                child.SetPivot(childOrientation);
                child.SetPosition(position, instant);
                position += child.Size * direction;
                maxSize = new Vector2(Mathf.Max(maxSize.x, child.Size.x), Mathf.Max(maxSize.y, child.Size.y));
                if (i + 1 < Children.Count) position += direction * Spacing;
                if (ExpandChildren || child.Expand) _expand.Push(child);
            }

            var contentSize = position - paddingPosition;

            while (_expand.Count > 0)
            {
                var child = _expand.Pop();
                child.ExpandSize(maxSize);
            }

            var contentWidth = Mathf.Max(Mathf.Abs(contentSize.x), maxSize.x, MinSize.x);
            var contentHeight = Mathf.Max(Mathf.Abs(contentSize.y), maxSize.y, MinSize.y); 
            AdjustSize(new Vector2(contentWidth, contentHeight));
        }

        private void FinishNewChildren()
        {
            while (_newChildren.Count > 0)
            {
                var child = _newChildren.Pop();
                child.FinishTransport();
            }
        }

        public override void FinishTransport()
        {
            foreach (var child in Children)
            {
                child.FinishTransport();
            }

            base.FinishTransport();
        }

        private void AdjustSize(Vector2 newSize)
        {
            if (newSize == _contentSize) return;
            _contentSize = newSize;
            if (Parent) Parent.IsDirty = true;
        }
    }
}