using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public abstract class ButtonElement : BackgroundElement, ISelectableElement
    {
        public Button Button { get; private set; }
        
        public Selectable Selectable => Button;

        public override void Init()
        {
            base.Init();
            Button = GetComponent<Button>();
        }

        public void AddListener(UnityAction action)
        {
            Button.onClick.AddListener(action);
        }
    }
}