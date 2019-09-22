using System;
using Bonwerk.Divvy.Data;
using Bonwerk.Divvy.Helpers;
using Bonwerk.Divvy.Styling;
using TMPro;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class TextElement : BackgroundElement, IFontElement, IContentTransform
    {
        [SerializeField] private TextStyle _style;
        [SerializeField] private TextMeshProUGUI _label;
        public TextMeshProUGUI Label => _label;

        public override ElementStyle ElementStyle => _style;

        public override Vector2 ContentSize => new Vector2(_label.preferredWidth, _label.preferredHeight);
        
        public RectTransform Content { get; private set; }
        
        public string Text
        {
            get => _label.text;
            set
            {
                if (_label.text == value) return;
                _label.text = value;
                if (Parent) Parent.LayoutDirty = true;
            }
        }

        public float Alpha
        {
            get => _label.color.a;
            set => _label.color = new Color(_label.color.r, _label.color.g, _label.color.b, value);
        }

        public bool RaycastTarget
        {
            get => _label.raycastTarget;
            set
            {
                if (_label.raycastTarget != value) _label.raycastTarget = value;
            }
        }

        public Color Foreground
        {
            get => _label.color;
            set => _label.color = value;
        }

        public override void Init()
        {
            base.Init();
            Content = Label.GetComponent<RectTransform>();
        }
    }
}