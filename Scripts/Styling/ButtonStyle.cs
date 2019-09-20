using UnityEngine;

namespace Bonwerk.Divvy.Styling
{
    public abstract class ButtonStyle : ElementStyle
    {
        [Header("Button")]
        [SerializeField] private Color _backgroundColor = Color.black;
        public Color BackgroundColor => _backgroundColor;

        [SerializeField] private bool _targetBackground;
        public bool TargetBackground => _targetBackground;
    }
}