using Bonwerk.Divvy.Data;
using Bonwerk.Divvy.Elements;
using UnityEngine;

namespace Bonwerk.Divvy.Styling
{
    [CreateAssetMenu(fileName = "DivStyle", menuName = "Divvy/DivStyle", order = 1)]
    public class DivStyle : ElementStyle
    {
        [Header("Div")]
        [SerializeField] private Dimensions _minSize;
        [SerializeField] private bool _expandChildren;
        [SerializeField] private float _spacing;
        [SerializeField] private Vector2 _childOrientation = new Vector2(0, 1);
        [SerializeField] private LayoutType _layout;

        public Dimensions MinSize => _minSize;
        public bool ExpandChildren => _expandChildren;
        public float Spacing => _spacing;
        public Vector2 ChildOrientation => _childOrientation;
        public LayoutType Layout => _layout;
    }
}