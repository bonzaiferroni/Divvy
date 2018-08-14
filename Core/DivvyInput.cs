using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Divvy.Core
{
    [RequireComponent(typeof(Image))]
    public class DivvyInput : DivvyPanel
    {
        [SerializeField] private TMP_InputField _input;
        [SerializeField] private bool _interactable = true;
        
        private TextMeshProUGUI _placeHolder;
        private Image _image;
        private string _lastValue;
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
                _lastValue = value;
                _input.text = value;
                Parent.ChildrenPositioned = false;
            }
        }

        public bool Interactable
        {
            get { return _interactable; }
            set
            {
                _interactable = value;
                UpdateInteractable(value);
            }
        }

        public override void Init()
        {
            _image = GetComponent<Image>();
            _placeHolder = _input.placeholder as TextMeshProUGUI;
            _input.onValueChanged.AddListener(_OnValueChanged);
            _input.onSubmit.AddListener(OnSubmit);
            _input.onDeselect.AddListener(OnSubmit);
            
            base.Init();
            UpdateInteractable(Interactable);
        }

        private void _OnValueChanged(string str)
        {
            Parent.ChildrenPositioned = false;
        }

        private void OnSubmit(string str)
        {
            StartCoroutine(UpdateNextFrame());
            if (str == _lastValue) return;
            _lastValue = str;
            OnValueChanged?.Invoke(str);
        }

        private IEnumerator UpdateNextFrame()
        {
            yield return null;
            UpdateInteractable(Interactable);
        }

        public void Activate()
        {
            UpdateInteractable(true);
            _input.ActivateInputField();
        }

        private void UpdateInteractable(bool interactable)
        {
            if (_input.interactable != interactable) _input.interactable = interactable;
            if (_image.enabled != interactable) _image.enabled = interactable;
        }
    }
}