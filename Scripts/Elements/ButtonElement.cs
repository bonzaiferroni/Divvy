using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public abstract class ButtonElement : BackgroundElement
    {
        public Button Button { get; private set; }
        
        public abstract ButtonStyle ButtonStyle { get; }
        public override BackgroundStyle BackgroundStyle => ButtonStyle;

        public override void Init()
        {
            base.Init();
            Button = GetComponent<Button>();
        }

        public void AddListener(UnityAction action)
        {
            Button.onClick.AddListener(action);
        }

        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            ApplyStyles.Selectable(Button, Background, ButtonStyle.Selectable);
        }
    }
}