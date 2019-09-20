using Bonwerk.Divvy.Styling;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public abstract class ButtonElement : Element
    {
        public Image Image { get; private set; }
        public Button Button { get; private set; }
        
        public abstract ButtonStyle ButtonStyle { get; }
        public override ElementStyle ElementStyle => ButtonStyle;

        public override void Init()
        {
            base.Init();
            Image = GetComponent<Image>();
            Button = GetComponent<Button>();

            if (Image)
            {
                Image.color = ButtonStyle.BackgroundColor;
                if (ButtonStyle.TargetBackground) Button.targetGraphic = Image;
            }
        }

        public void AddListener(UnityAction action)
        {
            Button.onClick.AddListener(action);
        }
    }
}