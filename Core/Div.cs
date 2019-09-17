using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bonwerk.Divvy.Core
{
    [ExecuteInEditMode]
    public class Div : Element
    {
        private readonly Stack<IElement> _newChildren = new Stack<IElement>();
        private readonly Stack<IElement> _expand = new Stack<IElement>();
        
        [Header("Div")]
        [SerializeField] private Spacing _padding;
        [SerializeField] private Dimensions _minSize;
        [SerializeField] private bool _reverseOrder;
        [SerializeField] private bool _expandChildren;
        [SerializeField] private float _lineHeight = -1;
        [SerializeField] private float _spacing;
        [SerializeField] private Vector2 _childOrientation = new Vector2(0, 1);
        [SerializeField] private LayoutStyle _style;
        
        public List<IElement> Children { get; } = new List<IElement>();
        
        public bool IsDirty { get; set; }
		
        public Spacing Padding => _padding;

        public Dimensions MinSize => _minSize;

        public Vector2 ChildOrientation => _childOrientation;

        public float Spacing => _spacing;

        public bool ExpandChildren => _expandChildren;

        public LayoutStyle Style
        {
            get => _style;
            set => _style = value;
        }

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
        
        public float LineHeight
        {
            get
            {
                if (_lineHeight < 0 && Parent != null) return Parent.LineHeight;
                return _lineHeight;
            }
            set { _lineHeight = value; }
        }

        // life cycle  

        public override void Init()
        {
            base.Init();
            FindChildren();
        }

        public void FindChildren()
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

        public override void UpdatePosition(bool instant)
        {
            foreach (var child in Children)
            {
                child.UpdatePosition(instant);
            }
            base.UpdatePosition(instant);
            PositionChildren(instant);
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
            child.SetPivot(ChildOrientation);
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
            if (!IsDirty) return;

            switch (Style)
            {
                case LayoutStyle.Vertical:
                    PositionVertical(instant);
                    break;
                case LayoutStyle.Horizontal:
                    PositionHorizontal(instant);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            FinishNewChildren();

            IsDirty = false;
        }

        private void PositionVertical(bool instant)
        {
            if (Style != LayoutStyle.Vertical) return;

            var topToBottom = ChildOrientation.y > 0; 
            
            var yPosition = topToBottom ? Padding.Top : Padding.Bottom;
            var maxWidth = 0f;

            var yOrientation = topToBottom ? -1 : 1;

            for (var i = 0; i < Children.Count; i++)
            {
                var index = ReverseOrder ? Children.Count - i - 1 : i;
                var child = Children[index];
                if (!child.IsVisible) continue;
                yPosition += topToBottom ? child.Margin.Top : child.Margin.Bottom;
                var xPos = Padding.Left + child.Margin.Left;
                child.Position.SetTargetPosition(new Vector2(xPos, yPosition * yOrientation), instant);
                yPosition += child.Height + (topToBottom ? child.Margin.Bottom : child.Margin.Top);
                var width = child.Margin.Left + child.Width + child.Margin.Right;
                if (width > maxWidth) maxWidth = width;
                if (i + 1 < Children.Count) yPosition += Spacing;
                if (ExpandChildren || child.Expand) _expand.Push(child);
            }

            yPosition += topToBottom ? Padding.Bottom : Padding.Top;

            while (_expand.Count > 0)
            {
                var child = _expand.Pop();
                child.ExpandWidth(maxWidth);
            }

            AdjustSize(maxWidth + Padding.Left + Padding.Right, yPosition);
        }

        private void PositionHorizontal(bool instant)
        {
            if (Style != LayoutStyle.Horizontal) return;
            
            var leftToRight = ChildOrientation.x < 1;
            
            var xPosition = leftToRight ? Padding.Left : Padding.Right;
            var maxHeight = 0f;
            
            var xOrientation = leftToRight ? 1 : -1;

            for (var i = 0; i < Children.Count; i++)
            {
                var index = ReverseOrder ? Children.Count - i - 1 : i;
                var child = Children[index];
                if (!child.IsVisible) continue;
                xPosition += leftToRight ? child.Margin.Left : child.Margin.Right;
                var yPos = Padding.Top + child.Margin.Top;
                child.Position.SetTargetPosition(new Vector2(xPosition * xOrientation, -yPos), instant);
                xPosition += child.Width + (leftToRight ? child.Margin.Right : child.Margin.Left);
                var height = child.Margin.Top + child.Height + child.Margin.Bottom;
                if (height > maxHeight) maxHeight = height;
                if (i + 1 < Children.Count) xPosition += Spacing;
                if (ExpandChildren || child.Expand) _expand.Push(child);
            }

            xPosition += leftToRight ? Padding.Right : Padding.Left;
            
            while (_expand.Count > 0)
            {
                var child = _expand.Pop();
                child.Height = maxHeight - (child.Margin.Top + child.Margin.Bottom);
            }

            AdjustSize(xPosition, maxHeight + Padding.Top + Padding.Bottom);
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

        private void AdjustSize(float width, float height)
        {
            width = Mathf.Max(width, MinSize.Width);
            height = Mathf.Max(height, MinSize.Height);
            if (width == Width && height == Height) return;
            Width = width;
            Height = height;
            if (Parent) Parent.IsDirty = true;
        }

        public override void ExpandWidth(float width)
        {
            base.ExpandWidth(width);
            if (!ExpandChildren || Style != LayoutStyle.Vertical) return;
            foreach (var child in Children)
            {
                child.ExpandWidth(width - (Padding.Left + Padding.Right));
            }
        }
    }
}