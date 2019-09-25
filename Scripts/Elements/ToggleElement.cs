using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public abstract class ToggleElement : BackgroundElement
    {
        [HideInInspector] [SerializeField] protected Toggle _toggle;
        [Header("Toggle")] [SerializeField] private SelectableStyle _selectableStyle;

        public bool IsOn
        {
            get => _toggle.isOn;
            set
            {
                if (_toggle.isOn != value) _toggle.isOn = value;
            }
        }

        protected abstract Toggle GetToggle();

        public event ToggleListener OnValueChanged;

        public override void Init()
        {
            base.Init();
            _toggle = GetToggle();
            _toggle.onValueChanged.AddListener(_OnValueChanged);
        }

        private void _OnValueChanged(bool isOn)
        {
            OnValueChanged?.Invoke(this, isOn);
        }
        
        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            ApplyStyles.Selectable(_toggle, _background, _selectableStyle);
        }
    }
    
    public delegate void ToggleListener(ToggleElement context, bool isOn);
}