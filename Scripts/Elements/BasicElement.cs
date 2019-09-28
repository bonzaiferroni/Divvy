using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class BasicElement : Element
    {
        [SerializeField] private Vector2 _contentSize;
        public override Vector2 ContentSize => _contentSize;
    }
}