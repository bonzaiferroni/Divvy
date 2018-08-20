using TMPro;
using UnityEngine;

namespace Divvy.Core
{
    public class DivvyText : Element
    {
        private TextMeshProUGUI _label;

        public override float Width
        {
            get
            {
                Rect.sizeDelta = new Vector2(_label.preferredWidth, Parent.LineHeight);
                return _label.preferredWidth;
            }
        }

        public override float Height
        {
            get
            {
                Rect.sizeDelta = new Vector2(Rect.sizeDelta.x, Parent.LineHeight);
                return Parent.LineHeight;
            }
        }

        public string Text
        {
            get { return _label.text; }
            set
            {
                if (_label.text == value) return;
                _label.text = value;
                Parent.ChildrenPositioned = false;
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
            _label = GetComponent<TextMeshProUGUI>(); // needs to come before base.Init()
            base.Init();
        }
    }
}