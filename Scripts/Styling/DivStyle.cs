using Bonwerk.Divvy.Data;
using Bonwerk.Divvy.Elements;
using UnityEngine;

namespace Bonwerk.Divvy.Styling
{
    [CreateAssetMenu(fileName = "Div", menuName = "Divvy/Div Style", order = 1)]
    public class DivStyle : BackgroundStyle
    {
        [Header("Div")] [SerializeField] private Vector2 _minSize;
        public Vector2 MinSize => _minSize;

        [SerializeField] private bool _expandChildren;
        public bool ExpandChildren => _expandChildren;

        [SerializeField] private float _spacing;
        public float Spacing => _spacing;

        [SerializeField] private Vector2 _childOrientation = new Vector2(0, 1);
        public Vector2 ChildOrientation => _childOrientation;

        [SerializeField] private LayoutType _layout;
        public LayoutType Layout => _layout;
    }
}