using Bonwerk.Divvy.Core;
using TMPro;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class DivText : Element
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private bool _overrideLineHeight;

        private Vector2 RectDelta
        {
            get
            {
                var height = _overrideLineHeight ? _label.preferredHeight : Parent.LineHeight;
                Transform.sizeDelta = new Vector2(_label.preferredWidth, height);
                return Transform.sizeDelta;
            }
        }

        public override float Width => RectDelta.x;

        public override float Height => RectDelta.y;

        public string Text
        {
            get { return _label.text; }
            set
            {
                if (_label.text == value) return;
                _label.text = value;
                if (Parent) Parent.IsDirty = true;
            }
        }

        public float Alpha
        {
            get { return _label.color.a; }
            set { _label.color = new Color(_label.color.r, _label.color.g, _label.color.b, value); }
        }

        public bool RaycastTarget
        {
            get { return _label.raycastTarget; }
            set
            {
                if (_label.raycastTarget != value) _label.raycastTarget = value;
            }
        }

        public override void Init()
        {
            base.Init();
            _label = GetComponent<TextMeshProUGUI>(); 
        }
    }
}