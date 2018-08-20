using Divvy.Core;
using FusionLib.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DivLib.Core
{
    public class DivRecipe
    {
        public static Color DarkBackgroundColor { get; } = new Color(.2f, .2f, .2f);
        public static Color LightBackgroundColor { get; } = new Color(.5f, .5f, .5f);
        public static Color TextColor { get; } = Color.white;
        public static Color ButtonColor { get; } = new Color(.2f, .4f, 1);
        public static Color InputColor { get; } = new Color(.2f, .6f, .4f);
        
        /*
         * Apply colors
         */
        
        public static void LightBackground(Image image)
        {
            image.color = LightBackgroundColor;
        }

        public static void DarkBackground(Image image)
        {
            image.color = DarkBackgroundColor;
        }
        
        public static void ButtonBackground(Image image)
        {
            image.color = ButtonColor;
        }
        
        public static void InputBackground(Image image)
        {
            image.color = InputColor;
        }
        
        /*
         * Text
         */
        
        public static void StyleText(TextMeshProUGUI tmp)
        {
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.autoSizeTextContainer = false;
            tmp.color = TextColor;
        }
        
        /*
         * Rect
         */

        public static void FillParent(Fusion obj)
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

        public static Color ChangeAlpha(Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }
        
        /*
         * Line
         */

        public static void LineParts(Fusion obj)
        {
            var div = obj.Add<Div>();
            div.Spacing = DivConstants.LineSpacing;
            div.Style = LayoutStyle.Horizontal;
            div.Padding.Left = div.Padding.Right = DivConstants.LineMargin;
        }
        
        /*
         * Input
         */
        
        public const string PlaceholderTag = "Placeholder";
        public const string TextTag = "InputText";
        public const string TextAreaTag = "TextArea";
        public const string InputChildTag = "InputChild";
        public const string InputTag = "Input";

        public static void InputParts(Fusion obj)
        {
            obj.NewChild(InputChildTag, InputChildParts);
            
            obj.Add<RectMask2D>();
            var image = obj.Add<Image>(InputBackground);
            var inputField = obj.Get<TMP_InputField>(InputChildTag);
            inputField.targetGraphic = image;
            obj.Add<DivInput>();
        }
        
        private static void InputTextParts(Fusion obj)
        {
            obj.Add<TextMeshProUGUI>(FormatInputText);
            FillParent(obj);
        }

        private static void PlaceholderParts(Fusion obj)
        {
            var label = obj.Add<TextMeshProUGUI>(FormatInputText);
            label.faceColor = ChangeAlpha(label.color, .5f);
            label.text = "Placeholder";
            FillParent(obj);
        }

        private static void FormatInputText(TextMeshProUGUI obj)
        {
            StyleText(obj);
            obj.margin = new Vector4(5, 0, 5, 0);
        }

        private static void TextAreaParts(Fusion obj)
        {
            obj.NewChild(PlaceholderTag, PlaceholderParts);
            obj.NewChild(TextTag, InputTextParts);
            obj.Add<RectMask2D>();
            FillParent(obj);
        }

        private static void InputChildParts(Fusion obj)
        {
            obj.NewChild(TextAreaTag, TextAreaParts);
            
            obj.Add<RectTransform>();
            var input = obj.Add<TMP_InputField>();
            input.placeholder = obj.Get<TextMeshProUGUI>(PlaceholderTag);
            input.textComponent = obj.Get<TextMeshProUGUI>(TextTag);
            input.textViewport = obj.Get<RectTransform>(TextAreaTag);

            obj.Rect.anchorMin = Vector2.zero;
            obj.Rect.anchorMax = Vector2.up;
            obj.Rect.pivot = Vector2.zero;
            obj.Rect.anchoredPosition = Vector2.zero;
            obj.Rect.sizeDelta = new Vector2(500, 0);
        }
        
        /*
         * Button
         */

        public static void ButtonParts(Fusion obj)
        {
            obj.NewChild(LabelTag, ButtonLabelParts);
            
            var div = obj.Add<Div>();
            div.MinSize = new Dimensions(90, 30);
            var image = obj.Add<Image>(ButtonBackground);
            obj.Add<Button>().targetGraphic = image;
        }

        private static void ButtonLabelParts(Fusion obj)
        {
            var label = obj.Add<TextMeshProUGUI>(StyleText);
            label.text = "Button";
            label.alignment = TextAlignmentOptions.Center;
            FillParent(obj);
        }
        
        /*
         * Label
         */
        
        public const string LabelTag = "Label";
        
        public static void LabelParts(Fusion obj)
        {
            obj.Add<DivText>();
            obj.Add<TextMeshProUGUI>(StyleText).text = "Label";
        }
        
        /*
         * LabelButton
         */

        public static void LabelButtonParts(Fusion obj)
        {
            obj.NewChild(LabelTag, LabelParts);
            obj.Add(LineParts);
            
            var image = obj.Add<Image>(ButtonBackground);
            obj.Add<Button>().targetGraphic = image;
        }
    }
}