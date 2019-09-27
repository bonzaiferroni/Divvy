using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class ToggleDiv : Div
    {
        [SerializeField] private bool _allowOff;
        
        public List<ToggleElement> Toggles { get; } = new List<ToggleElement>();

        private ToggleElement _current;
        public ToggleElement Current
        {
            get => _current;
            private set => SetCurrent(value);
        }

        public delegate void ToggleDivListener(ToggleElement context, bool isOn, int index);
        public event ToggleDivListener OnToggleChanged;
        
        protected override void Construct()
        {
            base.Construct();
            _current = null;
            OnToggleChanged = null;
        }

        protected override void Connect()
        {
            base.Connect();
            InitAllowOff();
        }

        public override void AddChild(IElement child, int index = -1, bool instantPositioning = true)
        {
            base.AddChild(child, index, instantPositioning);
            if (!(child is ToggleElement toggle)) return;
            Toggles.Add(toggle);
            toggle.OnValueChanged += _OnValueChanged;
            if (toggle.IsOn)
            {
                if (Current && !ReferenceEquals(Current, toggle))
                {
                    toggle.IsOn = false;
                }
                else
                {
                    Current = toggle;
                }
            }
        }

        public override void RemoveChild(IElement child)
        {
            base.RemoveChild(child);
            if (!(child is ToggleElement toggle)) return;
            Toggles.Remove(toggle);
            toggle.OnValueChanged -= _OnValueChanged;
            if (ReferenceEquals(Current, toggle)) Current = null;
            InitAllowOff();
        }

        public void ActivateIndex(int index)
        {
            Current = Toggles[index];
        }

        private void _OnValueChanged(ToggleElement context, bool isOn)
        {
            var index = Toggles.IndexOf(context);

            OnToggleChanged?.Invoke(context, isOn, index);

            if (isOn)
            {
                Current = context;
            }
            else
            {
                InitAllowOff();
            }
        }

        public void InitAllowOff()
        {
            if (_allowOff || Current || Toggles.Count == 0) return;
            ActivateIndex(0);
        }

        private void SetCurrent(ToggleElement value)
        {
            if (_current != null && !ReferenceEquals(_current, value))
            {
                _current.IsOn = false;
            }

            _current = value;
            if (_current != null && !_current.IsOn) _current.IsOn = true;
        }
    }
}