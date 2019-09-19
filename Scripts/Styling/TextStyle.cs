using UnityEngine;

namespace Bonwerk.Divvy.Styling
{
    [CreateAssetMenu(fileName = "TextStyle", menuName = "Divvy/TextStyle", order = 1)]
    public class TextStyle : ElementStyle
    {
        [SerializeField] private float _fontSize;
        
        public float FontSize => _fontSize;
    }
}