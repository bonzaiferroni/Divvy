using System;
using System.Collections.Generic;
using UnityEngine;

namespace DivLib.Core
{
    [ExecuteInEditMode]
    public class Div : Element
    {
        public Spacing Padding;
        public Dimensions MinSize;
        public float Spacing;
        public LayoutStyle Style;
        public bool ExpandChildren;
        
        [SerializeField] private bool _reversed;
        [SerializeField] private float _lineHeight = -1;
        
        private float _newHeight;
        private float _newWidth;
        private readonly Stack<Element> _newChildren = new Stack<Element>();
        private readonly Stack<Element> _expand = new Stack<Element>();

        public List<Element> Children { get; } = new List<Element>();
        public bool ChildrenPositioned { get; set; }

        public bool Reversed
        {
            get { return _reversed; }
            set
            {
                if (_reversed == value) return;
                _reversed = value;
                ChildrenPositioned = false;
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
                var child = childTransform.GetComponent<Element>();
                if (child == null) continue;
                child.Init();
                AddChild(child);
            }
            ChildrenPositioned = false;
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

        public void AddAfter(Element child, Element afterChild)
        {
            var index = Children.IndexOf(afterChild);
            if (index < 0) throw new Exception("Didn't find child to insert after");
            AddChild(child, index + 1);
        }
        
        public void AddChild(Element child, int index = -1, bool instantPositioning = true)
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
            if (child.transform != transform) child.transform.SetParent(transform, instantPositioning);
            if (instantPositioning) _newChildren.Push(child);
            ChildrenPositioned = false;
        }

        public void RemoveChild(Element child)
        {
            if (child.Parent != this) throw new Exception("Cannot remove child with a different parent");
            child.Parent = null;
            ChildrenPositioned = false;
            Children.Remove(child);
        }
        
        // Position Children

        private void PositionChildren(bool instant)
        {
            if (ChildrenPositioned) return;

            PositionVertical(instant);
            PositionHorizontal(instant);
            FinishNewChildren();

            ChildrenPositioned = true;
        }

        public IEnumerable<Element> Reverse()
        {
            for (int i = Children.Count - 1; i >= 0; i--)
            {
                var child = Children[i];
                yield return child;
            }
        }

        private void PositionVertical(bool instant)
        {
            if (Style != LayoutStyle.Vertical) return;
            
            var heightSum = Padding.Top;
            var maxWidth = 0f;
            var count = 0;

            var enumerable = Reversed ? Reverse() : Children;
            
            foreach (var child in enumerable)
            {
                if (!child.IsVisible) continue;
                heightSum += child.Margin.Top;
                var xPos = Padding.Left + child.Margin.Left;
                child.Position.SetTargetPosition(new Vector2(xPos, -heightSum), instant);
                heightSum += child.Height + child.Margin.Bottom;
                var width = child.Margin.Left + child.Width + child.Margin.Right;
                if (width > maxWidth) maxWidth = width;
                count++;
                if (count < Children.Count) heightSum += Spacing;
                if (ExpandChildren || child.ExpandSelf) _expand.Push(child);
            }

            heightSum += Padding.Bottom;

            while (_expand.Count > 0)
            {
                var child = _expand.Pop();
                child.ExpandWidth(maxWidth);
            }

            AdjustSize(maxWidth + Padding.Left + Padding.Right, heightSum);
        }

        private void PositionHorizontal(bool instant)
        {
            if (Style != LayoutStyle.Horizontal) return;
            
            var widthSum = Padding.Left;
            var maxHeight = 0f;
            var count = 0;
            
            var enumerable = Reversed ? Reverse() : Children;

            foreach (var child in enumerable)
            {
                if (!child.IsVisible) continue;
                widthSum += child.Margin.Left;
                var yPos = Padding.Top + child.Margin.Top;
                child.Position.SetTargetPosition(new Vector2(widthSum, -yPos), instant);
                widthSum += child.Width + child.Margin.Right;
                var height = child.Margin.Top + child.Height + child.Margin.Bottom;
                if (height > maxHeight) maxHeight = height;
                count++;
                if (count < Children.Count) widthSum += Spacing;
                if (ExpandChildren || child.ExpandSelf) _expand.Push(child);
            }

            widthSum += Padding.Right;
            
            while (_expand.Count > 0)
            {
                var child = _expand.Pop();
                child.Height = maxHeight - (child.Margin.Top + child.Margin.Bottom);
            }

            AdjustSize(widthSum, maxHeight + Padding.Top + Padding.Bottom);
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
            if (Parent) Parent.ChildrenPositioned = false;
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

        public void SetAsLast()
        {
            Parent.Children.Remove(this);
            Parent.Children.Add(this);
            Parent.ChildrenPositioned = false;
        }
    }

    [Serializable]
    public struct Spacing
    {
        public float Top;
        public float Right;
        public float Bottom;
        public float Left;

        public void Set(float all)
        {
            Top = Right = Bottom = Left = all;
        }

        public void Set(float topBottom, float leftRight)
        {
            Top = Bottom = topBottom;
            Left = Right = leftRight;
        }

        public void Set(float top, float right, float bottom, float left)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }
    }
    
    [Serializable]
    public struct Dimensions
    {
        public float Width;
        public float Height;

        public Dimensions(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public void Set(float width, float height)
        {
            Width = width;
            Height = height;
        }
    }
}