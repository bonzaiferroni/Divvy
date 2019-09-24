using System;
using Bonwerk.Divvy.Data;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    [Serializable]
    public class ElementStyle
    {
        [SerializeField] private Spacing _margin;
        public Spacing Margin => _margin;

        [SerializeField] private Spacing _padding;
        public Spacing Padding => _padding;

        [SerializeField] private RevealType _revealType = RevealType.Instant;
        public RevealType RevealType => _revealType;

        [SerializeField] private float _animationTime = .2f;
        public float AnimationTime => _animationTime;

        [SerializeField] private bool _easeAnimation;
        public bool EaseAnimation => _easeAnimation;

        [SerializeField] private bool _expand;
        public bool Expand => _expand;

        [SerializeField] private bool _isVisibleAtStart = true;
        public bool IsVisibleAtStart => _isVisibleAtStart;
    }
}