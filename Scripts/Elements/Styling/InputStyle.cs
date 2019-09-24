using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    [CreateAssetMenu(fileName = "Input", menuName = "Divvy/Input Style", order = 1)]
    public class InputStyle : BackgroundStyle
    {
        [Header("Input")]
        [SerializeField] private Vector2 _minSize;
        public Vector2 MinSize => _minSize;

        [SerializeField] private Vector2 _maxSize;
        public Vector2 MaxSize => _maxSize;

        [SerializeField] private FontProperties _text = new FontProperties(24, Color.white);
        public FontProperties Text => _text;
        
        [SerializeField] private FontProperties _placeholder = new FontProperties(24, new Color(1, 1, 1, .5f));
        public FontProperties Placeholder => _placeholder;
    }
}