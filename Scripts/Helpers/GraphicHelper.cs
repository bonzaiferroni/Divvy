using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy
{
    public static class GraphicHelper
    {
        public static void ChangeRgbOnly(this Graphic graphic, Color color)
        {
            var current = graphic.color;
            color.a = current.a;
            graphic.color = color;
        }

        public static void ChangeAlphaOnly(this Graphic graphic, float a)
        {
            var color = graphic.color;
            color.a = a;
            graphic.color = color;
        }
    }
}