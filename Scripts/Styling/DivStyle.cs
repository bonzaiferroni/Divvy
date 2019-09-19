using Bonwerk.Divvy.Data;
using Bonwerk.Divvy.Elements;
using UnityEngine;

namespace Bonwerk.Divvy.Styling
{
    public class DivStyle : ElementStyle
    {
        [Header("Div")]
        [SerializeField] private Spacing _padding;
        [SerializeField] private Dimensions _minSize;
        [SerializeField] private bool _reverseOrder;
        [SerializeField] private bool _expandChildren;
        [SerializeField] private float _lineHeight = -1;
        [SerializeField] private float _spacing;
        [SerializeField] private Vector2 _childOrientation = new Vector2(0, 1);
        [SerializeField] private LayoutStyle _style;

        public Spacing Padding => _padding;
        public Dimensions MinSize => _minSize;
        public bool ReverseOrder => _reverseOrder;
        public bool ExpandChildren => _expandChildren;
        public float LineHeight => _lineHeight;
        public float Spacing => _spacing;
        public Vector2 ChildOrientation => _childOrientation;
        public LayoutStyle Style => _style;
    }
}