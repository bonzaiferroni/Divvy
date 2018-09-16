using FusionLib.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DivLib.Core
{
    public class DivRecipe
    {
        public virtual Color DarkBackgroundColor { get; } = new Color(.3f, .3f, .3f);
        public virtual Color LightBackgroundColor { get; } = new Color(.4f, .4f, .4f);
        public virtual Color TextColor { get; } = Color.white;
        public virtual Color ButtonColor { get; } = new Color(.2f, .4f, 1);
        public virtual Color InputColor { get; } = new Color(.2f, .6f, .4f);
        public virtual Color ScrollHandleColor { get; } = new Color(.8f, .8f, .8f);

        public virtual float LineMargin => DivConstants.LineMargin;
        public virtual float LineSpacing => DivConstants.LineSpacing;
        public virtual float LineHeight => DivConstants.LineHeight;
        
        /*
         * Apply colors
         */
        
        public void LightBackground(Image image)
        {
            image.color = LightBackgroundColor;
        }

        public void DarkBackground(Image image)
        {
            image.color = DarkBackgroundColor;
        }
        
        public void ButtonBackground(Image image)
        {
            image.color = ButtonColor;
        }
        
        public void InputBackground(Image image)
        {
            image.color = InputColor;
        }
        
        /*
         * Text
         */
        
        public void StyleText(TextMeshProUGUI tmp)
        {
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.autoSizeTextContainer = false;
            tmp.faceColor = TextColor;
            // TODO: Fix fontsize / line height relationship in client code
            tmp.fontSize = LineHeight * .5f;
        }
        
        /*
         * Rect
         */

        public void FillParent(Fusion obj)
        {
            obj.Rect.anchorMin = Vector2.zero;
            obj.Rect.anchorMax = Vector2.one;
            obj.Rect.anchoredPosition = Vector2.zero;
            obj.Rect.sizeDelta = Vector2.zero;
            obj.Rect.pivot = new Vector2(.5f, .5f);
        }
        
        /*
         * Helpers
         */

        public Color ChangeAlpha(Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }
        
        /*
         * Line
         */

        public void LineParts(Fusion obj)
        {
            var div = obj.Add<Div>();
            div.Style = LayoutStyle.Horizontal;
            div.Spacing = LineSpacing;
            div.Padding.Left = div.Padding.Right = LineMargin;
        }

        public void LineMemberParts(Fusion obj)
        {
            var div = obj.Add<Div>();
            div.Style = LayoutStyle.Horizontal;
            div.Spacing = LineSpacing;
            div.Padding.Left = div.Padding.Right = LineSpacing;
        }
        
        /*
         * Input
         */
        
        public const string PlaceholderTag = "Placeholder";
        public const string TextTag = "InputText";
        public const string TextAreaTag = "TextArea";
        public const string InputChildTag = "InputChild";
        public const string InputTag = "Input";

        public void InputParts(Fusion obj)
        {
            obj.NewChild(InputChildTag, InputChildParts);
            
            obj.Add<RectMask2D>();
            var image = obj.Add<Image>(InputBackground);
            var inputField = obj.Get<TMP_InputField>(InputChildTag);
            inputField.targetGraphic = image;
            obj.Add<DivInput>();
        }
        
        private void InputTextParts(Fusion obj)
        {
            obj.Add<TextMeshProUGUI>(FormatInputText);
            FillParent(obj);
        }

        private void PlaceholderParts(Fusion obj)
        {
            var label = obj.Add<TextMeshProUGUI>(FormatInputText);
            label.faceColor = ChangeAlpha(label.color, .5f);
            label.text = "Placeholder";
            FillParent(obj);
        }

        private void FormatInputText(TextMeshProUGUI obj)
        {
            StyleText(obj);
            obj.margin = new Vector4(5, 0, 5, 0);
        }

        private void TextAreaParts(Fusion obj)
        {
            obj.NewChild(PlaceholderTag, PlaceholderParts);
            obj.NewChild(TextTag, InputTextParts);
            obj.Add<RectMask2D>();
            FillParent(obj);
        }

        public void InputChildParts(Fusion obj)
        {
            obj.NewChild(TextAreaTag, TextAreaParts);
            
            obj.Add<RectTransform>();
            var input = obj.Add<TMP_InputField>();
            input.placeholder = obj.Get<TextMeshProUGUI>(PlaceholderTag);
            input.textComponent = obj.Get<TextMeshProUGUI>(TextTag);
            input.textViewport = obj.Get<RectTransform>(TextAreaTag);
            input.selectionColor = Color.black;

            obj.Rect.anchorMin = Vector2.zero;
            obj.Rect.anchorMax = Vector2.up;
            obj.Rect.pivot = Vector2.zero;
            obj.Rect.anchoredPosition = Vector2.zero;
            obj.Rect.sizeDelta = new Vector2(500, 0);
        }
        
        /*
         * Button
         */

        public void ButtonParts(Fusion obj)
        {
            obj.NewChild(LabelTag, ButtonLabelParts);
            
            var div = obj.Add<Div>();
            div.MinSize = new Dimensions(90, 30);
            var image = obj.Add<Image>(ButtonBackground);
            obj.Add<Button>().targetGraphic = image;
        }

        private void ButtonLabelParts(Fusion obj)
        {
            var label = obj.Add<TextMeshProUGUI>(StyleText);
            label.text = "Button";
            label.alignment = TextAlignmentOptions.Center;
            FillParent(obj);
        }
        
        /*
         * IconButton
         */

        public void DivButtonParts(Fusion obj)
        {
            var button = obj.Add<DivButton>();
            button.MinSize.Set(LineHeight, LineHeight);
            var image = obj.Add<Image>();
            obj.Add<Button>().targetGraphic = image;
        }
        
        /*
         * Label
         */
        
        public const string LabelTag = "Label";
        
        public void LabelParts(Fusion obj)
        {
            obj.Add<DivText>();
            var label = obj.Add<TextMeshProUGUI>(StyleText);
            label.text = "Label";
            label.enableWordWrapping = false;
        }
        
        /*
         * LabelButton
         */

        public void LabelButtonParts(Fusion obj)
        {
            obj.NewChild(LabelTag, LabelParts);
            obj.Add(LineParts);
            
            var image = obj.Add<Image>(ButtonBackground);
            obj.Add<Button>().targetGraphic = image;
        }
        
        /*
         * DivScroll
         */

        public const string ScrollTag = "Scroll";
        public const string ViewportTag = "Viewport";
        public const string ContentTag = "Content";
        public const string HorizontalScrollbarTag = "Horizontal Scrollbar";
        public const string VerticalScrollbarTag = "Vertical Scrollbar";
        public const string SlidingAreaTag = "SlidingArea";
        public const string HandleTag = "Handle";

        public void ScrollParts(Fusion obj)
        {
            obj.NewChild(ViewportTag, ViewportParts);
            obj.NewChild(HorizontalScrollbarTag, HorizontalScrollbarParts);
            obj.NewChild(VerticalScrollbarTag, VerticalScrollbarParts);

            // obj.Add<Image>(DarkBackground);

            var scrollRect = obj.Add<ScrollRect>();
            scrollRect.content = obj.Get<RectTransform>(ContentTag);
            scrollRect.movementType = ScrollRect.MovementType.Clamped;
            scrollRect.viewport = obj.Get<RectTransform>(ViewportTag);
            scrollRect.verticalScrollbar = obj.Get<Scrollbar>(VerticalScrollbarTag);
            scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            scrollRect.verticalScrollbarSpacing = -3;
            scrollRect.horizontalScrollbar = obj.Get<Scrollbar>(HorizontalScrollbarTag);
            scrollRect.horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            scrollRect.horizontalScrollbarSpacing = -3;

            var divScroll = obj.Add<DivScroll>();
            divScroll.Style = LayoutStyle.Vertical;
            divScroll.Content = obj.Get<Div>(ContentTag);
            divScroll.ExpandChildren = true;
        }

        private void ViewportParts(Fusion obj)
        {
            obj.NewChild(ContentTag, ContentParts);

            obj.Add<Image>(DarkBackground);
            obj.Add<Mask>();

            FillParent(obj);
        }

        private void ContentParts(Fusion obj)
        {
            var div = obj.Add<Div>();
            div.ExpandChildren = true;
            div.Style = LayoutStyle.Vertical;
        }

        private void HorizontalScrollbarParts(Fusion obj)
        {
            obj.NewChild(SlidingAreaTag, SlidingAreaParts);

            var scrollbar = obj.Add<Scrollbar>();
            scrollbar.handleRect = obj.Get<RectTransform>(HandleTag);
            scrollbar.targetGraphic = obj.Get<Image>(HandleTag);
            scrollbar.direction = Scrollbar.Direction.LeftToRight;
            scrollbar.size = 1;
            
            var rect = obj.Rect;
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(1, 0);
            rect.pivot = Vector2.zero;
            rect.sizeDelta = new Vector2(0, 3);
        }

        private void VerticalScrollbarParts(Fusion obj)
        {
            obj.NewChild(SlidingAreaTag, SlidingAreaParts);

            var scrollbar = obj.Add<Scrollbar>();
            scrollbar.handleRect = obj.Get<RectTransform>(HandleTag);
            scrollbar.targetGraphic = obj.Get<Image>(HandleTag);
            scrollbar.direction = Scrollbar.Direction.BottomToTop;
            scrollbar.size = 1;

            var rect = obj.Rect;
            rect.anchorMin = new Vector2(1, 0);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = Vector2.one;
            rect.sizeDelta = new Vector2(3, 0);
        }

        private void SlidingAreaParts(Fusion obj)
        {
            obj.NewChild(HandleTag, HandleParts);

            obj.Add<RectTransform>();

            var rect = obj.Rect;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.pivot = new Vector2(.5f, .5f);
            rect.sizeDelta = Vector2.zero;
            rect.anchoredPosition = Vector2.zero;
        }

        private void HandleParts(Fusion obj)
        {
            obj.Add<Image>().color = ScrollHandleColor;

            var rect = obj.Rect;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.pivot = new Vector2(.5f, .5f);
            rect.sizeDelta = Vector2.zero;
            rect.anchoredPosition = Vector2.zero;
        }
    }
}