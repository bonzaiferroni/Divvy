using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class ToggleDiv : Div
    {
        [SerializeField] private bool _allowOff;
        
        private List<ToggleElement> Toggles { get; } = new List<ToggleElement>();

        private ToggleElement _current;
        private ToggleElement Current
        {
            get => _current;
            set => SetCurrent(value);
        }

        public event ToggleDivListener OnValueChanged;
        
        public override void Init()
        {
            base.Init();
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

        private void _OnValueChanged(ToggleElement context, bool isOn)
        {
            var index = -1;
            for (int i = 0; i < Toggles.Count; i++)
            {
                var toggle = Toggles[i];
                if (!ReferenceEquals(context, toggle)) continue;
                index = i;
                break;
            }

            OnValueChanged?.Invoke(context, isOn, index);

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
            Toggles[0].IsOn = true;
        }

        private void SetCurrent(ToggleElement value)
        {
            if (_current != null && !ReferenceEquals(_current, value))
            {
                _current.IsOn = false;
            }

            _current = value;
        }
    }

    public delegate void ToggleDivListener(ToggleElement context, bool isOn, int index);
}