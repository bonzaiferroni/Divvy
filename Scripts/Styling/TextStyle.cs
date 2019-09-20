using UnityEngine;

namespace Bonwerk.Divvy.Styling
{
    [CreateAssetMenu(fileName = "Text", menuName = "Divvy/Text Style", order = 1)]
    public class TextStyle : ElementStyle
    {
        [Header("Text")]
        [SerializeField] private float _fontSize = 24;
        public float FontSize => _fontSize;

        [SerializeField] private Color _fontColor = Color.white;
        public Color FontColor => _fontColor;
    }
}