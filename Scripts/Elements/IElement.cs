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
        
        // style
        Vector2 Size { get; }
        bool Expand { get; }
        
        void Init();

        void Refresh(bool instant);
        
        void SetPivot(Vector2 orientation);
        void SetPosition(Vector2 position, bool instant);
        
        void FinishTransport();
        void ExpandSize(Vector2 maxSize);
    }
}