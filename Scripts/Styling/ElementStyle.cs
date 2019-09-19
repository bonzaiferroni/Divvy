using Bonwerk.Divvy.Data;
using UnityEngine;

namespace Bonwerk.Divvy.Styling
{
    public abstract class ElementStyle : ScriptableObject
    {
        [Header("Element")]
        [SerializeField] private Spacing _margin;
        [SerializeField] private Spacing _padding;
        [SerializeField] private bool _expand;

        public Spacing Margin => _margin;
        public Spacing Padding => _padding;
        public bool Expand => _expand;
    }
}