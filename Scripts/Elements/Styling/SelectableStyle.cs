using System;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    [Serializable]
    public class SelectableStyle
    {
        [SerializeField] private bool _animateBackground;
        public bool AnimateBackground => _animateBackground;

        [SerializeField] private Color _normalColor = Color.white;
        public Color Normal => _normalColor;
        
        [SerializeField] private Color _highlightedColor = new Color(.95f, .95f, .95f, 1);
        public Color Highlighted => _highlightedColor;
        
        [SerializeField] private Color _pressedColor = new Color(.78f, .78f, .78f, 1);
        public Color Pressed => _pressedColor;
        
        [SerializeField] private Color _selectedColor = new Color(.95f, .95f, .95f, 1);
        public Color Selected => _selectedColor;
        
        [SerializeField] private Color _disabledColor = new Color(.78f, .78f, .78f, .5f);
        public Color Disabled => _disabledColor;
    }
}