using UnityEngine;

namespace Bonwerk.Divvy.Styling
{
    public interface IBackgroundStyle
    {
        Color BackgroundColor { get; }
        Sprite BackgroundSprite { get; }
    }
    
    public interface IFontStyle
    {
        float FontSize { get; }
        Color FontColor { get; }
    }

    public interface ISelectableStyle
    {
        bool AnimateBackground { get; }
    }
}