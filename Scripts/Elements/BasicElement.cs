using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class BasicElement : Element
    {
        [Header("Basic Element")][SerializeField] private Vector2 _contentSize;

        public override Vector2 ContentSize => _contentSize;

        public void SetSize(Vector2 size)
        {
            if (size == _contentSize) return;
            _contentSize = size;
            Parent?.SetLayoutDirty();
        }
    }
}