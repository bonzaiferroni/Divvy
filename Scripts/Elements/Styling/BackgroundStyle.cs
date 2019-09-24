using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public abstract class BackgroundStyle : ElementStyle, IBackgroundStyle
    {
        [Header("Background")] [SerializeField]
        private ImageProperties _background = new ImageProperties(Color.black);
        public ImageProperties Background => _background;
    }
}