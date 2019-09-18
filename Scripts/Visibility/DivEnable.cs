using System.Collections.Generic;
using Bonwerk.Divvy.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Visibility
{
    public class DivEnable : DivInstantVisibility
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