using Bonwerk.Divvy.Data;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public interface IElementStyle
    {
        Spacing Margin { get; }
        Spacing Padding { get; }
        RevealType RevealType { get; }
        bool Expand { get; }
    }
    
    public interface IBackgroundStyle : IElementStyle
    {
        ImageProperties Background { get; }
    }
}