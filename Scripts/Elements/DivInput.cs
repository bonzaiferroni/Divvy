using System;
using System.Collections;
using Bonwerk.Divvy.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class DivInput : Element
    {
        [SerializeField] private bool _interactable = true;
        [SerializeField] private bool _overrideLineHeight;
        
        private TMP_InputField _input;
        private TextMeshProUGUI _placeHolder;
        private Image _image;
        private string _lastValue;
        public event Action<string> OnValueChanged;

        private Vector2 RectDelta
        {
            get
            {
                var element = _input.text.Length > 0 ? _input.textComponent : _placeHolder;
                var height = _overrideLineHeight ? element.preferredHeight : Parent.LineHeight;
                Transform.sizeDelta = new Vector2(element.preferredWidth, height);
                return Transform.sizeDelta;
            }
        }

        public override float Width => RectDelta.x;

        public override float Height => RectDelta.y;

        public string Text
        {
            get { return _input.text; }
            set
            {
                if (_input.text == value) return;
                _lastValue = value;
                _input.text = value;
                Parent.IsDirty = true;
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
            base.Init();
            _input = GetComponentInChildren<TMP_InputField>();
            _image = GetComponent<Image>();
            _placeHolder = _input.placeholder as TextMeshProUGUI;
            _input.onValueChanged.AddListener(_OnValueChanged);
            _input.onSubmit.AddListener(OnSubmit);
            _input.onDeselect.AddListener(OnSubmit);
            
            UpdateInteractable(Interactable);
        }

        private void _OnValueChanged(string str)
        {
            Parent.IsDirty = true;
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
            if (!_image) return;
            if (_input.interactable != interactable) _input.interactable = interactable;
            if (_image.enabled != interactable) _image.enabled = interactable;
        }
    }
}