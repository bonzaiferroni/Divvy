using Bonwerk.Divvy.Data;
using UnityEngine;

namespace Bonwerk.Divvy.Styling
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
        Color BackgroundColor { get; }
        Sprite BackgroundSprite { get; }
    }
    
    public interface IFontStyle : IElementStyle
    {
        float FontSize { get; }
        Color FontColor { get; }
    }

    public interface ISelectableStyle : IElementStyle
    {
        bool AnimateBackground { get; }
    }
}