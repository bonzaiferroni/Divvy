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
        
        private readonly Stack<IElement> _newChildren = new Stack<IElement>();
        private readonly Stack<IElement> _expand = new Stack<IElement>();
        
        public List<IElement> Children { get; } = new List<IElement>();
        
        public bool IsDirty { get; set; }

        public Image BackgroundImage { get; private set; }

        public override Spacing Margin => _style.Margin;

        public override Spacing Padding => _style.Padding;
        
        public override bool Expand => _style.Expand;

        public Dimensions MinSize => _style.MinSize;

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

            switch (Layout)
            {
                case LayoutType.Vertical:
                    PositionVertical(instant);
                    break;
                case LayoutType.Horizontal:
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
            if (Layout != LayoutType.Vertical) return;

            var yPosition = Padding.Top;
            var maxWidth = 0f;

            for (var i = 0; i < Children.Count; i++)
            {
                var index = ReverseOrder ? Children.Count - i - 1 : i;
                var child = Children[index];
                if (!child.IsVisible) continue;
                yPosition += child.Margin.Top;
                var xPos = Padding.Left + child.Margin.Left;
                child.Position.SetTargetPosition(new Vector2(xPos, yPosition), instant);
                yPosition += child.Height + child.Margin.Bottom;
                var width = child.Margin.Left + child.Width + child.Margin.Right;
                if (width > maxWidth) maxWidth = width;
                if (i + 1 < Children.Count) yPosition += Spacing;
                if (ExpandChildren || child.Expand) _expand.Push(child);
            }

            yPosition += Padding.Bottom;

            while (_expand.Count > 0)
            {
                var child = _expand.Pop();
                child.ExpandWidth(maxWidth);
            }

            AdjustSize(maxWidth + Padding.Left + Padding.Right, yPosition);
        }

        private void PositionHorizontal(bool instant)
        {
            if (Layout != LayoutType.Horizontal) return;

            var xPosition = Padding.Left;
            var maxHeight = 0f;
            
            for (var i = 0; i < Children.Count; i++)
            {
                var index = ReverseOrder ? Children.Count - i - 1 : i;
                var child = Children[index];
                if (!child.IsVisible) continue;
                child.SetPivot(ChildOrientation);
                xPosition += child.Margin.Left;
                var yPos = Padding.Top + child.Margin.Top;
                child.Position.SetTargetPosition(new Vector2(xPosition, -yPos), instant);
                xPosition += child.Width + child.Margin.Right;
                var height = child.Margin.Top + child.Height + child.Margin.Bottom;
                if (height > maxHeight) maxHeight = height;
                if (i + 1 < Children.Count) xPosition += Spacing;
                if (ExpandChildren || child.Expand) _expand.Push(child);
            }

            xPosition += Padding.Right;
            
            while (_expand.Count > 0)
            {
                var child = _expand.Pop();
                child.ExpandHeight(maxHeight);
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
            if (!ExpandChildren || Layout != LayoutType.Vertical) return;
            foreach (var child in Children)
            {
                child.ExpandWidth(width - (Padding.Left + Padding.Right));
            }
        }
    }
}