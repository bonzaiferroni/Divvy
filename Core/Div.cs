﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace DivLib.Core
{
    [ExecuteInEditMode]
    public class Div : Element
    {
        public Spacing Padding;
        public Dimensions MinSize;
        public Vector2 ChildOrientation = new Vector2(0, 1);
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

        internal override void Init()
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
            if (!child.Initialized) child.Init();
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
            if (child.transform != transform) child.transform.SetParent(transform, false);
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
        
        public Element GetChild(string objectTag)
        {
            foreach (var child in Children)
            {
                if (child.name == objectTag) return child;
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
                if (child.name == objectTag && child is T) return child as T;
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

            var topToBottom = ChildOrientation.y > 0; 
            
            var heightSum = topToBottom ? Padding.Top : Padding.Bottom;
            var maxWidth = 0f;
            var count = 0;

            var enumerable = Reversed ? Reverse() : Children;

            var yOrientation = topToBottom ? -1 : 1;
            
            foreach (var child in enumerable)
            {
                if (!child.IsVisible) continue;
                heightSum += topToBottom ? child.Margin.Top : child.Margin.Bottom;
                var xPos = Padding.Left + child.Margin.Left;
                child.Position.SetTargetPosition(new Vector2(xPos, heightSum * yOrientation), instant);
                heightSum += child.Height + (topToBottom ? child.Margin.Bottom : child.Margin.Top);
                var width = child.Margin.Left + child.Width + child.Margin.Right;
                if (width > maxWidth) maxWidth = width;
                count++;
                if (count < Children.Count) heightSum += Spacing;
                if (ExpandChildren || child.ExpandSelf) _expand.Push(child);
            }

            heightSum += topToBottom ? Padding.Bottom : Padding.Top;

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
            
            var leftToRight = ChildOrientation.x < 1;
            
            var widthSum = leftToRight ? Padding.Left : Padding.Right;
            var maxHeight = 0f;
            var count = 0;
            
            var enumerable = Reversed ? Reverse() : Children;
            
            var xOrientation = leftToRight ? 1 : -1;

            foreach (var child in enumerable)
            {
                if (!child.IsVisible) continue;
                widthSum += leftToRight ? child.Margin.Left : child.Margin.Right;
                var yPos = Padding.Top + child.Margin.Top;
                child.Position.SetTargetPosition(new Vector2(widthSum * xOrientation, -yPos), instant);
                widthSum += child.Width + (leftToRight ? child.Margin.Right : child.Margin.Left);
                var height = child.Margin.Top + child.Height + child.Margin.Bottom;
                if (height > maxHeight) maxHeight = height;
                count++;
                if (count < Children.Count) widthSum += Spacing;
                if (ExpandChildren || child.ExpandSelf) _expand.Push(child);
            }

            widthSum += leftToRight ? Padding.Right : Padding.Left;
            
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