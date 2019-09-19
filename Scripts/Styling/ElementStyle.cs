using Bonwerk.Divvy.Data;
using UnityEngine;

namespace Bonwerk.Divvy.Styling
{
    public abstract class ElementStyle : ScriptableObject
    {
        [Header("Element")]
        [SerializeField] private Spacing _margin;
        [SerializeField] private float _height;
        [SerializeField] private float _width;
        [SerializeField] private bool _expand;

        public Spacing Margin => _margin;
        public float Height => _height;
        public float Width => _width;
        public bool Expand => _expand;
    }
}