using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public abstract class ButtonElement : BackgroundElement
    {
        [HideInInspector] [SerializeField] private Button _button;
        [Header("Button")] [SerializeField] private SelectableStyle _selectableStyle;

        protected override void Construct()
        {
            base.Construct();
            if (!_button) _button = GetComponent<Button>();
        }

        public void AddListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            ApplyStyles.Selectable(_button, _background, _selectableStyle);
        }
    }
}