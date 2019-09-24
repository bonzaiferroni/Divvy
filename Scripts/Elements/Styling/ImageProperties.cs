using System;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    [Serializable]
    public class ImageProperties
    {
        public ImageProperties(Color color)
        {
            _color = color;
        }

        [SerializeField] private Color _color;
        public Color Color => _color;

        [SerializeField] private Sprite _sprite;
        public Sprite Sprite => _sprite;
    }
}