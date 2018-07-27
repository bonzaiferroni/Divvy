using System.Collections.Generic;
using UnityEngine;

namespace Divvy.Core
{
    [ExecuteInEditMode]
    public class DivvyParent : DivvyElement
    {
        public LayoutStyle Style;
        private float _newHeight;
        private float _newWidth;
        
        private readonly Stack<DivvyElement> _finish = new Stack<DivvyElement>(); 

        public List<DivvyElement> Children { get; } = new List<DivvyElement>();
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
                var child = childTransform.GetComponent<DivvyElement>();
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
        
        public void AddChild(DivvyElement child)
        {
            if (child.Parent != this && child.Parent != null) child.Parent.RemoveElement(child);
            Children.Add(child);
            child.Parent = this;
            if (child.transform != transform) child.transform.SetParent(transform, false);
            _finish.Push(child);
            ChildrenPositioned = false;
        }

        public void RemoveElement(DivvyElement child)
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
            
            var heightSum = 0f;
            var maxWidth = 0f;
            foreach (var child in Children)
            {
                if (!child.IsVisible) continue;
                child.TargetPosition = new Vector2(0, -heightSum);
                heightSum += child.Height;
                if (child.Width > maxWidth) maxWidth = child.Width;
            }

            AdjustSize(maxWidth, heightSum);
        }

        private void PositionHorizontal()
        {
            if (Style != LayoutStyle.Horizontal) return;
            
            var widthSum = 0f;
            var maxHeight = 0f;
            foreach (var child in Children)
            {
                if (!child.IsVisible) continue;
                child.TargetPosition = new Vector2(widthSum, 0);
                widthSum += child.Width;
                if (child.Height > maxHeight) maxHeight = child.Height;
            }

            AdjustSize(widthSum, maxHeight);
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
}