using System;

namespace Bonwerk.Divvy.Core
{
    [Serializable]
    public struct Spacing
    {
        public float Top;
        public float Right;
        public float Bottom;
        public float Left;

        public void Set(float all)
        {
            Top = Right = Bottom = Left = all;
        }

        public void Set(float topBottom, float leftRight)
        {
            Top = Bottom = topBottom;
            Left = Right = leftRight;
        }

        public void Set(float top, float right, float bottom, float left)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }
    }
}