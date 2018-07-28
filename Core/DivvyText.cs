using TMPro;
using UnityEngine;

namespace Divvy.Core
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DivvyText : DivvyPanel
    {
        private TextMeshProUGUI _label;

        public override float Width
        {
            get
            {
                Rect.sizeDelta = new Vector2(_label.preferredWidth, Rect.sizeDelta.y);
                return _label.preferredWidth;
            }
        }

        public override float Height
        {
            get
            {
                Rect.sizeDelta = new Vector2(Rect.sizeDelta.x, Parent.ChildSize.y);
                return Parent.ChildSize.y;
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

        public override void Init()
        {
            base.Init();
            _label = GetComponent<TextMeshProUGUI>();
        }
    }
}