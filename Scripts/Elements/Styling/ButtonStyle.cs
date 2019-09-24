using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public abstract class ButtonStyle : BackgroundStyle
    {
        [Header("Button")]
        [SerializeField] private SelectableProperties _selectable = new SelectableProperties(false);
        public SelectableProperties Selectable => _selectable;
    }
}