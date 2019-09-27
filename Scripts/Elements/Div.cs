using System;
using System.Collections.Generic;
using Bonwerk.Divvy.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    [ExecuteInEditMode]
    public class Div : BackgroundElement, IParentElement
    {
        [Header("Div")] [SerializeField] private Vector2 _minSize;
        public Vector2 MinSize => _minSize;

        [SerializeField] private Vector2 _childOrientation = new Vector2(0, 1);
        public Vector2 ChildOrientation => _childOrientation;

        [SerializeField] private LayoutType _layout;
        public LayoutType Layout => _layout;

        [SerializeField] private bool _expandChildren;
        public bool ExpandChildren => _expandChildren;
        
        [SerializeField] private bool _reverseOrder;

        [SerializeField] private float _spacing;
        public float Spacing => _spacing;

        private Vector2 _contentSize;

        public List<IElement> Children { get; } = new List<IElement>();

        public bool LayoutDirty { get; private set; }

        public override Vector2 ContentSize => _contentSize;

        public bool ReverseOrder
        {
            get { return _reverseOrder; }
            set
            {
                if (_reverseOrder == value) return;
                _reverseOrder = value;
                SetLayoutDirty();
            }
        }

        // life cycle  

        protected override void Construct()
        {
            base.Construct();
            SetLayoutDirty();
        }

        protected override void Associate()
        {
            FindChildren();
        }

        private void FindChildren()
        {
            Children.Clear();
            for (var i = 0; i < transform.childCount; i++)
            {
                var childTransform = transform.GetChild(i);
                var child = childTransform.GetComponent<IElement>();
                if (child == null) continue;
                child.Init();
                AddChild(child);
            }
        }

        public override void Refresh(bool instant)
        {
            base.Refresh(instant);
            foreach (var child in Children)
            {
                child.Refresh(instant);
            }

            Rebuild(instant);
        }

        public override void Rebuild(bool instant)
        {
            var initialSize = _contentSize;
            while (LayoutDirty) 
            {
                LayoutDirty = false;
            
                foreach (var child in Children)
                {
                    child.Rebuild(instant);
                }
            
                PositionChildren(instant);    
            }

            if (_contentSize != initialSize)
            {
                Parent?.SetLayoutDirty();
                base.Rebuild(instant);
            }
        }

        // public

        public void AddAfter(IElement child, IElement afterChild)
        {
            var index = Children.IndexOf(afterChild);
            if (index < 0) throw new Exception("Didn't find child to insert after");
            AddChild(child, index + 1);
        }

        public virtual void AddChild(IElement child, int index = -1, bool instantPositioning = true)
        {
            child.Parent?.RemoveChild(child);
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
            SetLayoutDirty();
        }

        public virtual void RemoveChild(IElement child)
        {
            if (!ReferenceEquals(child.Parent, this)) throw new Exception("Cannot remove child with a different parent");
            child.Parent = null;
            SetLayoutDirty();
            Children.Remove(child);
        }

        public void SetLayoutDirty()
        {
            LayoutDirty = true;
        }

        // Position Children

        private void PositionChildren(bool instant)
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
                child.SetAnchor(childOrientation);
                child.SetPosition(position, instant);
                position += child.Size * direction;
                maxSize = new Vector2(Mathf.Max(maxSize.x, child.Size.x), Mathf.Max(maxSize.y, child.Size.y));
                if (i + 1 < Children.Count) position += direction * Spacing;
            }

            foreach (var child in Children)
            {
                if (!(ExpandChildren || child.Expand)) continue;
                if (Layout == LayoutType.Horizontal)
                {
                    child.ExpandSize(new Vector2(child.Size.x, maxSize.y), instant);
                }
                else
                {
                    child.ExpandSize(new Vector2(maxSize.x, child.Size.y), instant);
                }
            }

            var contentSize = position - paddingPosition;
            var contentWidth = Mathf.Max(Mathf.Abs(contentSize.x), maxSize.x, MinSize.x);
            var contentHeight = Mathf.Max(Mathf.Abs(contentSize.y), maxSize.y, MinSize.y);
            _contentSize = new Vector2(contentWidth, contentHeight);
        }

        public override void FinishTransport()
        {
            foreach (var child in Children)
            {
                child.FinishTransport();
            }

            base.FinishTransport();
        }
    }
}