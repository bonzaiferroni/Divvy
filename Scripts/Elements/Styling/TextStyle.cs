using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    [CreateAssetMenu(fileName = "Text", menuName = "Divvy/Text Style", order = 1)]
    public class TextStyle : BackgroundStyle
    {
        [SerializeField] private FontProperties _text;
        public FontProperties Text => _text;
    }
}