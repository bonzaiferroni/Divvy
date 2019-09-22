using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Visibility
{
    public class ElementEnable : InstantVisibility
    {
        [SerializeField] private List<Graphic> _items = new List<Graphic>();

        public void Add(Graphic graphic)
        {
            _items.Add(graphic);
        }

        protected override void Modify(bool isVisible)
        {
            foreach (var item in _items)
            {
                item.enabled = isVisible;
            }
        }
    }
}