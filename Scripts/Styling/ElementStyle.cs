using Bonwerk.Divvy.Data;
using UnityEngine;

namespace Bonwerk.Divvy.Styling
{
    public abstract class ElementStyle : ScriptableObject, IElementStyle
    {
        [Header("Element")]
        [SerializeField] private Spacing _margin;
        public Spacing Margin => _margin;
        
        [SerializeField] private Spacing _padding;
        public Spacing Padding => _padding;

        [SerializeField] private RevealType _revealType;
        public RevealType RevealType => _revealType;

        [SerializeField] private float _animationTime = .2f;
        public float AnimationTime => _animationTime;

        [SerializeField] private bool _expand;
        public bool Expand => _expand;
    }
}