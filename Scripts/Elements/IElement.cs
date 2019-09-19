using Bonwerk.Divvy.Data;
using Bonwerk.Divvy.Positioning;
using Bonwerk.Divvy.Styling;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public interface IElement
    {
        bool IsVisible { get; }
        string Name { get; }
        string Tag { get; }
        Div Parent { get; set; }
        RectTransform Transform { get; }
        DivPosition Position { get; }
        
        // style
        Spacing Margin { get; }
        Spacing Padding { get; }
        float Height { get; }
        float Width { get; }
        bool Expand { get; }
        
        void Init();
        void UpdatePosition(bool instant);
        void SetPivot(Vector2 orientation);
        void ExpandWidth(float parentWidth);
        void ExpandHeight(float maxHeight);
        void FinishTransport();
    }
}