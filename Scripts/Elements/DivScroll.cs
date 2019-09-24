using System.Collections.Generic;
using Bonwerk.Divvy.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class DivScroll : BackgroundElement, IParentElement, IContentElement
    {
        [SerializeField] private DivScrollStyle _style;
        public override ElementStyle ElementStyle => _style;
        
        [SerializeField] private DivElement _div;
        public DivElement Div => _div;

        [SerializeField] private ScrollRect _scrollRect;
        public ScrollRect ScrollRect => _scrollRect;
        
        public RectTransform Content { get; private set; }
        public Image ScrollBackground { get; private set; }
        
        public List<IElement> Children => _div.Children;

        public override Vector2 ContentSize => _style.ContentSize;

        public override void Init()
        {
            base.Init();
            Content = ScrollRect.GetComponent<RectTransform>();
            ScrollBackground = ScrollRect.GetComponent<Image>();
            Div.Parent = this;
            Div.Init();
        }


        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            ScrollRect.movementType = _style.Movement;
            ApplyStyles.Image(ScrollBackground, _style.ScrollBackground);
            ApplyStyles.Image(ScrollRect.horizontalScrollbar.GetComponent<Image>(), _style.ScrollBar);
            ApplyStyles.Image(ScrollRect.horizontalScrollbar.handleRect.GetComponent<Image>(), _style.ScrollHandle);
            ApplyStyles.Image(ScrollRect.verticalScrollbar.GetComponent<Image>(), _style.ScrollBar);
            ApplyStyles.Image(ScrollRect.verticalScrollbar.handleRect.GetComponent<Image>(), _style.ScrollHandle);
            ScrollRect.horizontalScrollbar.GetComponent<RectTransform>().sizeDelta = new Vector2(0, _style.HandleWidth);
            ScrollRect.verticalScrollbar.GetComponent<RectTransform>().sizeDelta = new Vector2(_style.HandleWidth, 0);
        }

        public void SetLayoutDirty()
        {
            // nothing
        }

        public void AddChild(IElement child, int index = -1, bool instantPositioning = true)
        {
            Div.AddChild(child, index, instantPositioning);
        }

        public void RemoveChild(IElement child)
        {
            Div.RemoveChild(child);
        }

    }
}