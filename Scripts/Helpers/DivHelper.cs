using Bonwerk.Divvy.Elements;

namespace Bonwerk.Divvy.Helpers
{
    public static class DivHelper
    {

        public static IElement GetChild(Div div, string objectTag)
        {
            foreach (var child in div.Children)
            {
                if (child.Name == objectTag) return child;
            }

            foreach (var child in div.Children)
            {
                var childDiv = child as Div;
                if (childDiv == null) continue;
                var grandChild = GetChild(childDiv, objectTag);
                if (grandChild == null) continue;
                return grandChild;
            }

            return null;
        }

        public static T GetChild<T>(Div div, string objectTag) where T : class, IElement
        {
            foreach (var child in div.Children)
            {
                if (child.Name == objectTag && child is T) return child as T;
            }

            foreach (var child in div.Children)
            {
                var childDiv = child as Div;
                if (childDiv == null) continue;
                var grandChild = GetChild<T>(childDiv, objectTag);
                if (grandChild == null) continue;
                return grandChild;
            }

            return null;
        }
    }
}