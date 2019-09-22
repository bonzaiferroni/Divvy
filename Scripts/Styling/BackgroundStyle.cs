using UnityEngine;

namespace Bonwerk.Divvy.Styling
{
    public abstract class BackgroundStyle : ElementStyle, IBackgroundStyle
    {
        [Header("Background")]
        [SerializeField] private Sprite _backgroundSprite;
        public Sprite BackgroundSprite => _backgroundSprite;
        
        [SerializeField] private Color _backgroundColor = Color.black;
        public Color BackgroundColor => _backgroundColor;
    }
}