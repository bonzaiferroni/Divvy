using TMPro;
using UnityEngine;

namespace Divvy.Core
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DivvyText : DivvyElement
    {
        private TextMeshProUGUI _label;

        public override void Init()
        {
            base.Init();
            _label = GetComponent<TextMeshProUGUI>();
        }

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
    }
}