using UnityEngine;

namespace Bonwerk.Divvy.Styling
{
    [CreateAssetMenu(fileName = "Input", menuName = "Divvy/Input Style", order = 1)]
    public class InputStyle : FontStyle
    {
        [Header("Input")]
        [SerializeField] private Vector2 _minSize;
        public Vector2 MinSize => _minSize;

        [SerializeField] private Vector2 _maxSize;
        public Vector2 MaxSize => _maxSize;
    }
}