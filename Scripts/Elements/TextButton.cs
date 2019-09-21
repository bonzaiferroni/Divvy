using Bonwerk.Divvy.Helpers;
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
        
        public RectTransform ContentTransform { get; private set; }

        public override void Init()
        {
            base.Init();
            
            // label
            _label.fontSize = _style.FontSize;
            _label.color = _style.FontColor;

            ContentTransform = _label.GetComponent<RectTransform>();
        }

        public override void SetSize(bool instant)
        {
            base.SetSize(instant);
            ContentTransform.SetPadding(_style.Padding);
        }
    }
}