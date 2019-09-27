using System;
using System.Collections.Generic;
using Bonwerk.Divvy.Data;
using Bonwerk.Divvy.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class Element : MonoBehaviour, IElement
    {
        [HideInInspector] [SerializeField] protected RectTransform _contentRect;
        
        [Header("Element")] [SerializeField] private ElementStyle _elementStyle;
        public ElementStyle ElementStyle => _elementStyle;
        public bool Expand => ElementStyle.Expand;
        public Spacing Margin => ElementStyle.Margin;
        public Spacing Padding => ElementStyle.Padding;
        
        public bool IsVisible => Revealer.IsVisible;

        public IParentElement Parent { get; set; }

        public RectTransform Transform { get; private set; }
        public ElementRevealer Revealer { get; private set; }
        public ElementPositioner Positioner { get; private set; }
        public ElementSizer Sizer { get; private set; }
        public bool StyleDirty { get; protected set; }

        public string Name => gameObject.name;
        public string Tag => gameObject.tag;
        public Vector2 Size => PaddedSize + new Vector2(Margin.Left + Margin.Right, Margin.Top + Margin.Bottom);
        public Vector2 PaddedSize => Sizer.Target;
        
        private GraphicList Graphics { get; } = new GraphicList();
        
        public abstract Vector2 ContentSize { get; }

        // called once at DivRoot.Awake() or instantiation
        public void Init()
        {
            Construct();
            Connect();
            ApplyStyle(true);
            Revealer.SetVisibility(ElementStyle.IsVisibleAtStart, true);
        }

        protected virtual void Construct()
        {
            Transform = GetComponent<RectTransform>();
            Positioner = CreatePositioner();
            Sizer = CreateSizer();
            Revealer = CreateRevealer();
            StyleDirty = true;
            Parent = null;
        }

        protected virtual void Connect()
        {
            Revealer.OnVisibilityChange += OnVisibilityChange;
        }

        // called every frame
        public virtual void Refresh(bool instant)
        {
            if (StyleDirty) ApplyStyle(instant);
            if (IsVisible && Positioner.Transporting) Positioner.Refresh(instant || Revealer.CurrentVisibility == 0);
            if (Revealer.Transitioning) Revealer.Refresh(instant);
            if (Sizer.Resizing) Sizer.Refresh(instant);
        }

        protected virtual void ApplyStyle(bool instant)
        {
            Graphics.Clear();
            StyleDirty = false;
        }

        private void OnVisibilityChange(IElement element, bool isVisible)
        {
            Parent?.SetLayoutDirty();
        }

        public virtual void FinishTransport()
        {
            Positioner.FinishTransport();
        }

        // called when Parent.LayoutDirty == true
        public void SetPosition(Vector2 position, bool instant)
        {
            var pivot = Transform.pivot;
            position += new Vector2(pivot.x * PaddedSize.x, (1 - pivot.y) * -PaddedSize.y);
            var anchor = Transform.anchorMin;
            var marginX = (1 - anchor.x) * Margin.Left + anchor.x * -Margin.Right;
            var marginY = (1 - anchor.y) * Margin.Bottom + anchor.y * -Margin.Top;
            position += new Vector2(marginX, marginY);
            Positioner.SetTargetPosition(position, instant);
        }

        // called when Parent.LayoutDirty == true
        public virtual void Rebuild(bool instant)
        {
            SetSize(ContentSize + new Vector2(Padding.Left + Padding.Right, Padding.Top + Padding.Bottom), instant);
        }

        protected virtual void SetSize(Vector2 size, bool instant)
        {
            if (_contentRect)
            {
                _contentRect.SetPadding(Padding);
            }

            Sizer.SetTargetSize(size, instant);
        }

        public void SetAnchor(Vector2 anchor)
        {
            if (anchor == Transform.anchorMin && anchor == Transform.anchorMax) return;
            Transform.anchorMin = Transform.anchorMax = anchor;
        }

        public virtual void ExpandSize(Vector2 size, bool instant)
        {
            SetSize(size - new Vector2(Margin.Right + Margin.Left, Margin.Top + Margin.Bottom), instant);
        }

        private ElementRevealer CreateRevealer()
        {
            switch (ElementStyle.RevealType)
            {
                case RevealType.Instant:
                    return new InstantRevealer(this, Graphics);
                case RevealType.Fade:
                    return new FadeRevealer(this, Graphics, ElementStyle.AnimationTime, ElementStyle.EaseAnimation);
                case RevealType.Scale:
                    return new ScaleRevealer(this, transform, ElementStyle.AnimationTime, ElementStyle.EaseAnimation);
                case RevealType.Canvas:
                    return new CanvasRevealer(this, GetComponent<CanvasGroup>(), ElementStyle.AnimationTime,
                        ElementStyle.EaseAnimation);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public ElementPositioner CreatePositioner()
        {
            switch (ElementStyle.PositioningAnimationType)
            {
                case AnimationType.Instant:
                    return new InstantPositioner(Transform);
                case AnimationType.Direct:
                    return new DirectPositioner(Transform, ElementStyle.AnimationTime);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public ElementSizer CreateSizer()
        {
            switch (ElementStyle.PositioningAnimationType)
            {
                case AnimationType.Instant:
                    return new InstantSizer(Transform);
                case AnimationType.Direct:
                    return new DirectSizer(Transform, ElementStyle.AnimationTime);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected void AddGraphic(TMP_Text label, FontStyle style)
        {
            ApplyStyles.Font(label, style);
            Graphics.Add(new ElementGraphic(label, style));
        }

        protected void AddGraphic(Image image, ImageStyle style)
        {
            ApplyStyles.Image(image, style);
            Graphics.Add(new ElementGraphic(image, style));
        }
    }

    public enum LayoutType
    {
        Vertical,
        Horizontal,
    }
}