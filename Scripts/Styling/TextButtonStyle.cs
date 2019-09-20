using UnityEngine;

namespace Bonwerk.Divvy.Styling
{
    [CreateAssetMenu(fileName = "TextButton", menuName = "Divvy/TextButton Style", order = 1)]
    public class TextButtonStyle : ButtonStyle
    {
        [Header("Text Button")]
        [SerializeField] private float _fontSize = 24;
        public float FontSize => _fontSize;
        
        [SerializeField] private Color _fontColor = Color.white;
        public Color FontColor => _fontColor;
    }
}