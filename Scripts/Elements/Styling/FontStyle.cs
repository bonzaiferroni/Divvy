using System;
using TMPro;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    [Serializable]
    public class FontStyle
    {
        public FontStyle()
        {
            _size = 24;
            _color = Color.white;
        }
        
        public FontStyle(float size, Color color)
        {
            _size = size;
            _color = color;
        }

        [SerializeField] private float _size;
        public float Size => _size;

        [SerializeField] private Color _color;
        public Color Color => _color;

        [SerializeField] private TMP_FontAsset _font;
        public TMP_FontAsset Font => _font;
    }
}