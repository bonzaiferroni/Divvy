using Bonwerk.Divvy.Data;
using Bonwerk.Divvy.Styling;
using TMPro;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class TextElement : Element
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private TextStyle _style;

        public override ElementStyle ElementStyle => _style;

        public override Vector2 ContentSize => new Vector2(_label.preferredWidth, _label.preferredHeight);
        
        public string Text
        {
            get => _label.text;
            set
            {
                if (_label.text == value) return;
                _label.text = value;
                if (Parent) Parent.IsDirty = true;
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
            _label = GetComponent<TextMeshProUGUI>();
            _label.fontSize = _style.FontSize;
            _label.color = _style.FontColor;
        }
        
        public override void SetSize(bool instant)
        {
            _label.margin = new Vector4(_style.Padding.Left, _style.Padding.Top, _style.Padding.Right, _style.Padding.Bottom);
            PaddedSize = ContentSize;
        }
    }
}