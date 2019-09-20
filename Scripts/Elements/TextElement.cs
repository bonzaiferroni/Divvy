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

        public override bool Expand => _style.Expand;
        public override Spacing Margin => _style.Margin;
        public override Spacing Padding => _style.Padding;

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
        }
    }
}