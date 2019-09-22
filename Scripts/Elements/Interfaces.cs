using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public interface IBackgroundElement
    {
        Image Background { get; }
    }
    
    public interface IFontElement
    {
        TextMeshProUGUI Label { get; }
    }

    public interface ISelectableElement
    {
        Selectable Selectable { get; }
    }

    public interface IContentTransform
    {
        RectTransform Content { get; }
    }
}