using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public abstract class ButtonStyle : BackgroundStyle, ISelectableStyle
    {
        [Header("Selectable")]
        [SerializeField] private bool _animateBackground;
        public bool AnimateBackground => _animateBackground;
    }
}