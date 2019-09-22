using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public abstract class BackgroundElement : Element, IBackgroundElement
    {
        public Image Background { get; private set; }

        public override void Init()
        {
            base.Init();
            Background = GetComponent<Image>();
        }
    }
}