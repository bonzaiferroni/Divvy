using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DivLib.Core
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
                Rect.sizeDelta = new Vector2(element.preferredWidth, height);
                return Rect.sizeDelta;
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

        internal override void Init()
        {
            _input = GetComponentInChildren<TMP_InputField>();
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
            if (!_image) return;
            if (_input.interactable != interactable) _input.interactable = interactable;
            if (_image.enabled != interactable) _image.enabled = interactable;
        }
    }
}