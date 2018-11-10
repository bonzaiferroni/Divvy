using TMPro;
using UnityEngine;

namespace DivLib.Core
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
                Rect.sizeDelta = new Vector2(_label.preferredWidth, height);
                return Rect.sizeDelta;
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
                if (Parent) Parent.ChildrenPositioned = false;
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

        internal override void Init()
        {
            _label = GetComponent<TextMeshProUGUI>(); // needs to come before base.Init()
            base.Init();
        }
    }
}