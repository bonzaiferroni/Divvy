using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public interface IBackgroundElement : IElement
    {
        Image Background { get; }
    }
    
    public interface IFontElement : IElement
    {
        TextMeshProUGUI Label { get; }
    }

    public interface ISelectableElement : IElement
    {
        Selectable Selectable { get; }
    }

    public interface IContentElement : IElement
    {
        RectTransform Content { get; }
    }
}