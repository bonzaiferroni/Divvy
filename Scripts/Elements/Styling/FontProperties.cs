using System;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    [Serializable]
    public class FontProperties
    {
        public FontProperties(float size, Color color)
        {
            _size = size;
            _color = color;
        }

        [SerializeField] private float _size;
        public float Size => _size;

        [SerializeField] private Color _color;
        public Color Color => _color;
    }
}