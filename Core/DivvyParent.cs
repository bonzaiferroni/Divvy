﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Divvy.Core
{
    [ExecuteInEditMode]
    public class DivvyParent : DivvyPanel
    {
        public Padding Padding;
        public float Spacing;
        public LayoutStyle Style;
        [SerializeField] private bool _reversed;
        [SerializeField] private Vector2 _childSize;
        
        private float _newHeight;
        private float _newWidth;
        private readonly Stack<DivvyPanel> _newChildren = new Stack<DivvyPanel>();

        public List<DivvyPanel> Children { get; } = new List<DivvyPanel>();
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
        
        public Vector2 ChildSize
        {
            get
            {
                if (_childSize == Vector2.zero && Parent != null) return Parent.ChildSize;
                return _childSize;
            }
        }

        // life cycle 

        public override void Init()
        {
            base.Init();
            Children.Clear();
            _newChildren.Clear();
            for (var i = 0; i < transform.childCount; i++)
            {
                var childTransform = transform.GetChild(i);
                var child = childTransform.GetComponent<DivvyPanel>();
                if (child == null) continue;
                child.Init();
                AddChild(child);
            }
            ChildrenPositioned = false;
        }

        protected override void Update()
        {
            base.Update();
            PositionChildren(false);
        }
        
        // public

        public void AddAfter(DivvyPanel child, DivvyPanel afterChild)
        {
            var index = Children.IndexOf(afterChild);
            if (index < 0) throw new Exception("Didn't find child to insert after");
            AddChild(child, index + 1);
        }
        
        public void AddChild(DivvyPanel child, int index = -1)
        {
            if (child.Parent != this && child.Parent != null) child.Parent.RemoveChild(child);
            if (index >= 0)
            {
                Children.Insert(index, child);
            }
            else
            {
                Children.Add(child);
            }
            
            child.Parent = this;
            if (child.transform != transform) child.transform.SetParent(transform, false);
            _newChildren.Push(child);
            ChildrenPositioned = false;
        }

        public void RemoveChild(DivvyPanel child)
        {
            if (child.Parent == this) child.Parent = null;
            ChildrenPositioned = false;
            Children.Remove(child);
        }
        
        // Position Children

        public override void FinishTransport()
        {
            foreach (var child in Children)
            {
                child.FinishTransport();
            }

            PositionChildren(true);
            base.FinishTransport();
        }

        private void PositionChildren(bool instant)
        {
            if (ChildrenPositioned) return;

            PositionVertical(instant);
            PositionHorizontal(instant);
            FinishNewChildren();

            ChildrenPositioned = true;
        }

        public IEnumerable<DivvyPanel> Reverse()
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
                child.SetTargetPosition(new Vector2(Padding.Left, -heightSum), instant);
                heightSum += child.Height;
                if (child.Width > maxWidth) maxWidth = child.Width;
                count++;
                if (count < Children.Count) heightSum += Spacing;
            }

            heightSum += Padding.Bottom;

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
                child.SetTargetPosition(new Vector2(widthSum, -Padding.Top), instant);
                widthSum += child.Width;
                if (child.Height > maxHeight) maxHeight = child.Height;
                count++;
                if (count < Children.Count) widthSum += Spacing;
            }

            widthSum += Padding.Right;

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

        private void AdjustSize(float width, float height)
        {
            if (width == Width && height == Height) return;
            Width = width;
            Height = height;
            if (Parent) Parent.ChildrenPositioned = false;
        }
    }

    [Serializable]
    public struct Padding
    {
        public float Top;
        public float Right;
        public float Bottom;
        public float Left;
    }
}