using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public interface IGraphicStyle
    {
        Color Color { get; }
        bool RaycastTarget { get; }
    }
}