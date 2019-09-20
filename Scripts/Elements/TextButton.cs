using Bonwerk.Divvy.Styling;
using TMPro;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class TextButton : ButtonElement
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private TextButtonStyle _style;

        public override ButtonStyle ButtonStyle => _style;

        public override Vector2 ContentSize => new Vector2(_label.preferredWidth, _label.preferredHeight);

        public override void Init()
        {
            base.Init();
            
            // label
            _label.fontSize = _style.FontSize;
            _label.color = _style.FontColor;
            _label.margin = new Vector4(_style.Padding.Left, _style.Padding.Top, _style.Padding.Right, _style.Padding.Bottom);
            _label.SetLayoutDirty();
        }

        public override void SetSize(bool instant)
        {
            PaddedSize = ContentSize;
        }
    }
}