using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    [CreateAssetMenu(fileName = "Image", menuName = "Divvy/Image Style", order = 1)]
    public class ImageStyle : BackgroundStyle
    {
        [Header("Sprite Button")]
        [SerializeField] private Color _spriteColor = Color.white;
        public Color SpriteColor => _spriteColor;

        [SerializeField] private Sprite _sprite;
        public Sprite Sprite => _sprite;

        [SerializeField] private Vector2 _size = new Vector2(128, 128);
        public Vector2 Size => _size;
    }
}