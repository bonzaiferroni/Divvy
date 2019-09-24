using System;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    [Serializable]
    public class SelectableProperties
    {
        public SelectableProperties(bool animateBackground)
        {
            _animateBackground = animateBackground;
        }

        [SerializeField] private bool _animateBackground;
        public bool AnimateBackground => _animateBackground;
    }
}