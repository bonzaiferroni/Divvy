using System;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    [Serializable]
    public class ImageStyle : IGraphicStyle
    {
        public ImageStyle()
        {
            _color = Color.white;
        }
        
        public ImageStyle(Color color)
        {
            _color = color;
        }

        [SerializeField] private Color _color;
        public Color Color => _color;

        [SerializeField] private Sprite _sprite;
        public Sprite Sprite => _sprite;
        
        [SerializeField] private bool _raycastTarget = true;
        public bool RaycastTarget => _raycastTarget;
    }
}