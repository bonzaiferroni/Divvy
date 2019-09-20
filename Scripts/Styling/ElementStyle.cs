using Bonwerk.Divvy.Data;
using UnityEngine;

namespace Bonwerk.Divvy.Styling
{
    public abstract class ElementStyle : ScriptableObject
    {
        [Header("Element")]
        [SerializeField] private Spacing _margin;
        public Spacing Margin => _margin;
        
        [SerializeField] private Spacing _padding;
        public Spacing Padding => _padding;

        [SerializeField] private bool _expand;
        public bool Expand => _expand;
    }
}