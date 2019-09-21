using Bonwerk.Divvy.Styling;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public abstract class ButtonElement : Element
    {
        public Image BackgroundImage { get; private set; }
        public Button Button { get; private set; }
        
        public abstract ButtonStyle ButtonStyle { get; }
        public override ElementStyle ElementStyle => ButtonStyle;

        public override void Init()
        {
            base.Init();
            BackgroundImage = GetComponent<Image>();
            Button = GetComponent<Button>();

            if (BackgroundImage)
            {
                BackgroundImage.color = ButtonStyle.BackgroundColor;
                BackgroundImage.sprite = ButtonStyle.BackgroundSprite;
                if (ButtonStyle.TargetBackground) Button.targetGraphic = BackgroundImage;
            }
        }

        public void AddListener(UnityAction action)
        {
            Button.onClick.AddListener(action);
        }
    }
}