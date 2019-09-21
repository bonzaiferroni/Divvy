using Bonwerk.Divvy.Data;
using UnityEngine;

namespace Bonwerk.Divvy.Helpers
{
    public static class TransformHelper
    {
        public static void SetPadding(this RectTransform rt, Spacing padding)
        {
            rt.offsetMin = new Vector2(padding.Left, padding.Bottom);
            rt.offsetMax = new Vector2(-padding.Right, -padding.Top);
        }
    }
}