using UnityEngine;

namespace Bonwerk.Divvy.Styling
{
    public abstract class ButtonStyle : ElementStyle
    {
        [SerializeField] private Color _backgroundColor = Color.black;
        public Color BackgroundColor => _backgroundColor;
    }
}