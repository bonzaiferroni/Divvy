using System;

namespace Bonwerk.Divvy.Core
{
    [Serializable]
    public struct Dimensions
    {
        public float Width;
        public float Height;

        public Dimensions(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public void Set(float width, float height)
        {
            Width = width;
            Height = height;
        }
    }
}