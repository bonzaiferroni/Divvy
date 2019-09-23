using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    [CreateAssetMenu(fileName = "SpriteButton", menuName = "Divvy/SpriteButton Style", order = 1)]
    public class SpriteButtonStyle : ButtonStyle
    {
        [Header("Sprite Button")]
        [SerializeField] private Color _spriteColor = Color.white;
        public Color SpriteColor => _spriteColor;

        [SerializeField] private Sprite _sprite;
        public Sprite Sprite => _sprite;

        [SerializeField] private Vector2 _size = new Vector2(32, 32);
        public Vector2 Size => _size;
    }
}