using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    [CreateAssetMenu(fileName = "TextButton", menuName = "Divvy/TextButton Style", order = 1)]
    public class TextButtonStyle : ButtonStyle
    {
        [Header("Text Button")] 
        [SerializeField] private FontProperties _text;
        public FontProperties Text => _text;
    }
}