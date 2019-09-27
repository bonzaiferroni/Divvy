using System;
using System.Collections;
using System.Linq;
using Bonwerk.Divvy.Data;
using Bonwerk.Divvy.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class InputElement : BackgroundElement
    {
        
        [HideInInspector] [SerializeField] private TMP_InputField _input;
        [Header("Input Field")] [SerializeField] private FontStyle _textStyle = new FontStyle(24, Color.white);
        public FontStyle TextStyle => _textStyle;
        [SerializeField] private FontStyle _placeholderStyle = new FontStyle(24, new Color(1, 1, 1, .5f));
        public FontStyle PlaceholderStyle => _placeholderStyle;
        [SerializeField] private SelectableStyle _selectableStyle;
        public SelectableStyle SelectableStyle => _selectableStyle;
        [SerializeField] private Vector2 _minSize;
        public Vector2 MinSize => _minSize;
        [SerializeField] private Vector2 _maxSize;
        public Vector2 MaxSize => _maxSize;
        
        private string LastValue { get; set; }
        private IEnumerator _UpdateNextFrame;
        
        public event Action<string> OnValueChanged;
        
        public override Vector2 ContentSize => GetContentSize();

        public string Text
        {
            get => _input.text;
            set
            {
                if (_input.text == value) return;
                LastValue = value;
                _input.text = value;
                Parent.SetLayoutDirty();
            }
        }

        [SerializeField] private bool _interactable = true;
        public bool Interactable
        {
            get => _interactable;
            set
            {
                _interactable = value;
                UpdateInteractable(value);
            }
        }

        protected override void Construct()
        {
            base.Construct();
            if (!_input) _input = GetComponent<TMP_InputField>();
            if (!_contentRect) _contentRect = this.GetAndValidate<RectTransform>("Text Area");
        }

        protected override void Connect()
        {
            base.Connect();
            _input.onValueChanged.AddListener(_OnValueChanged);
            _input.onSubmit.AddListener(OnSubmit);
            _input.onDeselect.AddListener(OnSubmit);
            
            _UpdateNextFrame = UpdateNextFrame();
            UpdateInteractable(Interactable);
        }

        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            AddGraphic(_input.textComponent, TextStyle);
            AddGraphic(_input.placeholder as TMP_Text, PlaceholderStyle);
            ApplyStyles.Selectable(_input, _background, SelectableStyle);
        }

        private void _OnValueChanged(string str)
        {
            Parent.SetLayoutDirty();
        }

        private void OnSubmit(string str)
        {
            StartCoroutine(_UpdateNextFrame);
            if (str == LastValue) return;
            LastValue = str;
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
            if (!_background) return;
            if (_input.interactable != interactable) _input.interactable = interactable;
            if (_background.enabled != interactable) _background.enabled = interactable;
        }

        private Vector2 GetContentSize()
        {
            var element = _input.text.Length > 0 ? _input.textComponent : (TMP_Text) _input.placeholder;
            var width = element.preferredWidth;
            if (_input.text.Length > 0 && char.IsWhiteSpace(_input.text.LastOrDefault()))
                width += element.fontSize / 2;
            var height = element.preferredHeight;
            if (MinSize.x > 0) width = Mathf.Max(MinSize.x, width);
            if (MinSize.y > 0) height = Mathf.Max(MinSize.y, height);
            if (MaxSize.x > 0) width = Mathf.Min(MaxSize.x, width);
            if (MaxSize.y > 0) height = Mathf.Min(MaxSize.y, height);
            
            return new Vector2(width, height);
        }
    }
}