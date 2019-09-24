using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public abstract class BackgroundElement : Element, IBackgroundElement
    {
        public Image Background { get; private set; }
        
        public abstract BackgroundStyle BackgroundStyle { get; }

        public override ElementStyle ElementStyle => BackgroundStyle;

        public override void Init()
        {
            base.Init();
            Background = GetComponent<Image>();
            if (Background) ApplyStyles.Image(Background, BackgroundStyle.Background);
        }
    }
}