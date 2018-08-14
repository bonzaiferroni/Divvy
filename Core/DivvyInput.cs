using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Divvy.Core
{
    public class DivvyInput : DivvyPanel
    {
        [SerializeField] private TMP_InputField _input;
        
        private TextMeshProUGUI _placeHolder;
        public event Action<string> OnValueChanged;

        public override float Width
        {
            get
            {
                var width = _input.text.Length > 0 ? _input.textComponent.preferredWidth : _placeHolder.preferredWidth;
                Rect.sizeDelta = new Vector2(width, Parent.ChildSize.y);
                return width;
            }
        }

        public override float Height
        {
            get
            {
                Rect.sizeDelta = new Vector2(Rect.sizeDelta.x, Parent.ChildSize.y);
                return Parent.ChildSize.y;
            }
        }

        public string Text
        {
            get { return _input.text; }
            set
            {
                if (_input.text == value) return;
                _input.text = value;
                Parent.ChildrenPositioned = false;
            }
        }

        public override void Init()
        {
            _placeHolder = _input.placeholder as TextMeshProUGUI;
            _input.onValueChanged.AddListener(_OnValueChanged);
            base.Init();
        }

        private void _OnValueChanged(string str)
        {
            Parent.ChildrenPositioned = false;
            OnValueChanged?.Invoke(str);
        }

        public void Activate()
        {
            _input.ActivateInputField();
        }
    }
}