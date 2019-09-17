using UnityEngine;

namespace Bonwerk.Divvy.Core
{
    public interface IElement
    {
        bool Initialized { get; }
        bool IsVisible { get; }
        Div Parent { get; set; }
        RectTransform Transform { get; }
        string Name { get; }
        Spacing Margin { get; }
        DivPosition Position { get; }
        float Height { get; set; }
        float Width { get; set; }
        bool Expand { get; }
        
        void Init();
        void UpdatePosition(bool instant);
        void SetPivot(Vector2 orientation);
        void ExpandWidth(float parentWidth);
        void FinishTransport();
    }
}