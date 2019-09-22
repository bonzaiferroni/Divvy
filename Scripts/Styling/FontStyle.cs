using UnityEngine;

namespace Bonwerk.Divvy.Styling
{
    public abstract class FontStyle : BackgroundStyle, IFontStyle
    {
        [Header("Font")]
        [SerializeField] private float _fontSize = 24;
        public float FontSize => _fontSize;

        [SerializeField] private Color _fontColor = Color.white;
        public Color FontColor => _fontColor;
    }
}