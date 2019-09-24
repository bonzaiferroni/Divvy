using System.Collections.Generic;

namespace Bonwerk.Divvy.Elements
{
    public interface IParentElement : IElement
    {
        List<IElement> Children { get; }
        
        void SetLayoutDirty();
        
        void AddChild(IElement child, int index = -1, bool instantPositioning = true);
        void RemoveChild(IElement child);
    }
}