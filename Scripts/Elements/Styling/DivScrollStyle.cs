using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    [CreateAssetMenu(fileName = "DivScroll", menuName = "Divvy/DivScroll Style", order = 1)]
    public class DivScrollStyle : BackgroundStyle
    {
        [SerializeField] private Vector2 _contentSize = new Vector2(200, 100);
        public Vector2 ContentSize => _contentSize;

        [SerializeField] private ScrollRect.MovementType _movement = ScrollRect.MovementType.Elastic;
        public ScrollRect.MovementType Movement => _movement;

        [SerializeField] private ImageProperties _scrollBackground = new ImageProperties(new Color(1, 1, 1, .4f));
        public ImageProperties ScrollBackground => _scrollBackground;

        [SerializeField] private ImageProperties _scrollBar = new ImageProperties(Color.white);
        public ImageProperties ScrollBar => _scrollBar;

        [SerializeField] private ImageProperties _scrollHandle = new ImageProperties(Color.white);
        public ImageProperties ScrollHandle => _scrollHandle;

        [SerializeField] private float _handleWidth = 20;
        public float HandleWidth => _handleWidth;
    }
}