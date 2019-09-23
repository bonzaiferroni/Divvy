namespace Bonwerk.Divvy.Elements
{
    public static class ApplyStyles
    {
        public static void Background(Element e, ElementStyle s)
        {
            if (!(e is IBackgroundElement element) || !(s is IBackgroundStyle style)) return;
            if (!element.Background) return;
            element.Background.color = style.BackgroundColor;
            element.Background.sprite = style.BackgroundSprite;
        }

        public static void Font(Element e, ElementStyle s)
        {
            if (!(e is IFontElement element) || !(s is IFontStyle style)) return;
            if (!element.Label) return;
            element.Label.fontSize = style.FontSize;
            element.Label.color = style.FontColor;
        }

        public static void Selectable(Element e, ElementStyle s)
        {
            if (!(e is ISelectableElement element) || !(s is ISelectableStyle style)) return;
            var background = (e as IBackgroundElement)?.Background;
            
            if (background)
            {
                if (style.AnimateBackground) element.Selectable.targetGraphic = background;
            }
        }
    }
}