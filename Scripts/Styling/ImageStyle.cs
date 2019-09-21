using UnityEngine;

namespace Bonwerk.Divvy.Styling
{
    [CreateAssetMenu(fileName = "Image", menuName = "Divvy/Image Style", order = 1)]
    public class ImageStyle : ElementStyle
    {
        [Header("Sprite Button")]
        [SerializeField] private Color _spriteColor = Color.white;
        public Color SpriteColor => _spriteColor;

        [SerializeField] private Color _backgroundColor = Color.black;
        public Color BackgroundColor => _backgroundColor;

        [SerializeField] private Sprite _sprite;
        public Sprite Sprite => _sprite;

        [SerializeField] private Sprite _backgroundSprite;
        public Sprite BackgroundSprite => _backgroundSprite;

        [SerializeField] private Vector2 _size = new Vector2(128, 128);
        public Vector2 Size => _size;
    }
}