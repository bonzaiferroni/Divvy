using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public abstract class ButtonElement : BackgroundElement, ISelectableElement
    {
        [SerializeField] private Button _button;
        public Button Button => _button;
        public Selectable Selectable => _button;

        public void AddListener(UnityAction action)
        {
            Button.onClick.AddListener(action);
        }
    }
}