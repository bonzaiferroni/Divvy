using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public interface IBackgroundElement : IElement
    {
        Image Background { get; }
    }

    public interface IContentElement : IElement
    {
        RectTransform Content { get; }
    }
}