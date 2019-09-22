using System;
using System.Collections;
using System.Linq;
using Bonwerk.Divvy.Data;
using Bonwerk.Divvy.Helpers;
using Bonwerk.Divvy.Styling;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class InputElement : BackgroundElement, IContentElement
    {
        [SerializeField] private InputStyle _style;
        [SerializeField] private RectTransform _content;
        public RectTransform Content => _content;
        
        private TMP_InputField Input { get; set; }
        private string LastValue { get; set; }
        private IEnumerator _UpdateNextFrame;
        
        public event Action<string> OnValueChanged;

        public override ElementStyle ElementStyle => _style;
        
        public override Vector2 ContentSize => GetContentSize();
        
        private TextMeshProUGUI PlaceholderText => (TextMeshProUGUI) Input.placeholder;

        public string Text
        {
            get => Input.text;
            set
            {
                if (Input.text == value) return;
                LastValue = value;
                Input.text = value;
                Parent.LayoutDirty = true;
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

        public override void Init()
        {
            base.Init();
            Input = GetComponent<TMP_InputField>();
            Input.onValueChanged.AddListener(_OnValueChanged);
            Input.onSubmit.AddListener(OnSubmit);
            Input.onDeselect.AddListener(OnSubmit);

            _UpdateNextFrame = UpdateNextFrame();
            UpdateInteractable(Interactable);
        }

        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            Input.textComponent.fontSize = _style.FontSize;
            PlaceholderText.fontSize = _style.FontSize;
            Input.textComponent.color = _style.FontColor;
            PlaceholderText.color = new Color(_style.FontColor.r, _style.FontColor.g, _style.FontColor.b, .5f);
        }

        private void _OnValueChanged(string str)
        {
            Parent.LayoutDirty = true;
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
            Input.ActivateInputField();
        }

        private void UpdateInteractable(bool interactable)
        {
            if (!Background) return;
            if (Input.interactable != interactable) Input.interactable = interactable;
            if (Background.enabled != interactable) Background.enabled = interactable;
        }

        private Vector2 GetContentSize()
        {
            var element = Input.text.Length > 0 ? Input.textComponent : PlaceholderText;
            var width = element.preferredWidth;
            if (Input.text.Length > 0 && char.IsWhiteSpace(Input.text.LastOrDefault()))
                width += element.fontSize / 2;
            var height = element.preferredHeight;
            if (_style.MinSize.x > 0) width = Mathf.Max(_style.MinSize.x, width);
            if (_style.MinSize.y > 0) height = Mathf.Max(_style.MinSize.y, height);
            if (_style.MaxSize.x > 0) width = Mathf.Min(_style.MaxSize.x, width);
            if (_style.MaxSize.y > 0) height = Mathf.Min(_style.MaxSize.y, height);
            
            return new Vector2(width, height);
        }
    }
}