using TMPro;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public static class ApplyStyles
    {
        public static void Image(Image image, ImageStyle style)
        {
            image.color = style.Color;
            image.sprite = style.Sprite;
        }

        public static void Font(TMP_Text label, FontStyle style)
        {
            label.fontSize = style.Size;
            label.color = style.Color;
            label.font = style.Font;
        }

        public static void Selectable(Selectable selectable, Image background, SelectableStyle style)
        {
            if (background && style.AnimateBackground)
            {
                selectable.targetGraphic = background;
            }

            var colors = selectable.colors;
            colors.normalColor = style.Normal;
            colors.highlightedColor = style.Highlighted;
            colors.pressedColor = style.Pressed;
            colors.selectedColor = style.Selected;
            colors.disabledColor = style.Disabled;
            selectable.colors = colors;
        }
    }
}