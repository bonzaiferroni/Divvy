using UnityEngine;
using UnityEngine.Events;
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

        public delegate void ToggleListener(ToggleElement context, bool isOn);
        public event ToggleListener OnValueChanged;

        protected override void Construct()
        {
            base.Construct();
            OnValueChanged = null;
            _toggle = GetToggle();
            _toggle.onValueChanged.RemoveAllListeners();
        }

        protected override void Associate()
        {
            base.Associate();
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

        public void AddListener(UnityAction<bool> listener)
        {
            _toggle.onValueChanged.AddListener(listener);
        }

        public void RemoveListener(UnityAction<bool> listener)
        {
            _toggle.onValueChanged.RemoveListener(listener);
        }

        public void AddToGroup(ToggleGroup toggleGroup)
        {
            toggleGroup.RegisterToggle(_toggle);
        }
    }
}