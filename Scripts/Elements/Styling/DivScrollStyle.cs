using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    [CreateAssetMenu(fileName = "DivScroll", menuName = "Divvy/DivScroll Style", order = 1)]
    public class DivScrollStyle : BackgroundStyle
    {
        [SerializeField] private Vector2 _contentSize = new Vector2(200, 100);
        public Vector2 ContentSize => _contentSize;

        [SerializeField] private ImageProperties _scrollBackground = new ImageProperties(new Color(1, 1, 1, .4f));
        public ImageProperties ScrollBackground => _scrollBackground;
    }
}