using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class DivScroll : BackgroundElement, IParentElement
    {
        [Header("DivScroll")]
        [HideInInspector] [SerializeField] private Div _div;
        [HideInInspector] [SerializeField] private ScrollRect _scrollRect;
        [HideInInspector] [SerializeField] private Image _scrollBackground;
        
        [SerializeField] private Vector2 _contentSize = new Vector2(200, 100);
        public override Vector2 ContentSize => _contentSize;
        
        [SerializeField] private ScrollRect.MovementType _movementType = ScrollRect.MovementType.Elastic;
        public ScrollRect.MovementType MovementType => _movementType;

        [SerializeField] private ImageStyle _scrollBackgroundStyle = new ImageStyle(new Color(1, 1, 1, .4f));
        public ImageStyle ScrollBackgroundStyle => _scrollBackgroundStyle;

        [SerializeField] private ImageStyle _scrollBarStyle = new ImageStyle(Color.white);
        public ImageStyle ScrollBarStyle => _scrollBarStyle;

        [SerializeField] private ImageStyle _scrollHandleStyle = new ImageStyle(Color.white);
        public ImageStyle ScrollHandleStyle => _scrollHandleStyle;

        [SerializeField] private float _handleWidth = 20;
        public float HandleWidth => _handleWidth;

        public List<IElement> Children => _div.Children;

        protected override void Construct()
        {
            base.Construct();
            if (!_div) _div = GetComponentInChildren<Div>();
            if (!_scrollRect) _scrollRect = GetComponentInChildren<ScrollRect>();
            if (!_scrollBackground) _scrollBackground = _scrollRect.GetComponent<Image>();
            if (!_contentRect) _contentRect = _scrollRect.GetComponent<RectTransform>();
        }

        protected override void Connect()
        {
            base.Connect();
            _div.Parent = this;
            _div.Init();
        }

        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            _scrollRect.movementType = MovementType;
            AddGraphic(_scrollBackground, ScrollBackgroundStyle);
            AddGraphic(_scrollRect.horizontalScrollbar.GetComponent<Image>(), ScrollBarStyle);
            AddGraphic(_scrollRect.horizontalScrollbar.handleRect.GetComponent<Image>(), ScrollHandleStyle);
            AddGraphic(_scrollRect.verticalScrollbar.GetComponent<Image>(), ScrollBarStyle);
            AddGraphic(_scrollRect.verticalScrollbar.handleRect.GetComponent<Image>(), ScrollHandleStyle);
            _scrollRect.horizontalScrollbar.GetComponent<RectTransform>().sizeDelta = new Vector2(0, HandleWidth);
            _scrollRect.verticalScrollbar.GetComponent<RectTransform>().sizeDelta = new Vector2(HandleWidth, 0);
        }

        public override void Refresh(bool instant)
        {
            base.Refresh(instant);
            _div.Refresh(instant);
        }

        public override void Rebuild(bool instant)
        {
            base.Rebuild(instant);
            _div.Rebuild(instant);
        }

        public void SetLayoutDirty()
        {
            // nothing
        }

        public void AddChild(IElement child, int index = -1, bool instantPositioning = true)
        {
            _div.AddChild(child, index, instantPositioning);
        }

        public void RemoveChild(IElement child)
        {
            _div.RemoveChild(child);
        }

    }
}