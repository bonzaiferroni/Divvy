using System;
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
        public Vector2 ChildSize;
        public LayoutStyle Style;
        private float _newHeight;
        private float _newWidth;
        
        private readonly Stack<DivvyPanel> _finish = new Stack<DivvyPanel>(); 

        public List<DivvyPanel> Children { get; } = new List<DivvyPanel>();
        public bool ChildrenPositioned { get; set; }

        // life cycle 

        public override void Init()
        {
            base.Init();
            Children.Clear();
            _finish.Clear();
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
            PositionChildren();
        }
        
        // public
        
        public void AddChild(DivvyPanel child)
        {
            if (child.Parent != this && child.Parent != null) child.Parent.RemoveElement(child);
            Children.Add(child);
            child.Parent = this;
            if (child.transform != transform) child.transform.SetParent(transform, false);
            _finish.Push(child);
            ChildrenPositioned = false;
        }

        public void RemoveElement(DivvyPanel child)
        {
            Children.Remove(child);
            if (child.Parent == this) child.Parent = null;
            ChildrenPositioned = false;
        }
        
        // Position Children

        private void PositionChildren()
        {
            if (ChildrenPositioned) return;

            PositionVertical();
            PositionHorizontal();
            Finish();

            ChildrenPositioned = true;
        }

        private void PositionVertical()
        {
            if (Style != LayoutStyle.Vertical) return;
            
            var heightSum = Padding.Top;
            var maxWidth = 0f;
            var count = 0;
            foreach (var child in Children)
            {
                if (!child.IsVisible) continue;
                child.TargetPosition = new Vector2(Padding.Left, -heightSum);
                heightSum += child.Height;
                if (child.Width > maxWidth) maxWidth = child.Width;
                count++;
                if (count < Children.Count) heightSum += Spacing;
            }

            heightSum += Padding.Bottom;

            AdjustSize(maxWidth + Padding.Left + Padding.Right, heightSum);
        }

        private void PositionHorizontal()
        {
            if (Style != LayoutStyle.Horizontal) return;
            
            var widthSum = Padding.Left;
            var maxHeight = 0f;
            var count = 0;
            foreach (var child in Children)
            {
                if (!child.IsVisible) continue;
                child.TargetPosition = new Vector2(widthSum, Padding.Top);
                widthSum += child.Width;
                if (child.Height > maxHeight) maxHeight = child.Height;
                count++;
                if (count < Children.Count) widthSum += Spacing;
            }

            widthSum += Padding.Right;

            AdjustSize(widthSum, maxHeight + Padding.Top + Padding.Bottom);
        }

        private void Finish()
        {
            while (_finish.Count > 0)
            {
                var child = _finish.Pop();
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