using TMPro;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public static class ApplyStyles
    {
        public static void Image(Image image, ImageProperties style)
        {
            image.color = style.Color;
            image.sprite = style.Sprite;
        }

        public static void Font(TMP_Text label, FontProperties style)
        {
            label.fontSize = style.Size;
            label.color = style.Color;
        }

        public static void Selectable(Selectable selectable, Image background, SelectableProperties style)
        {
            if (background && style.AnimateBackground)
            {
                selectable.targetGraphic = background;
            }
        }
    }
}