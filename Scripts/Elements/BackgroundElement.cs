using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public abstract class BackgroundElement : Element
    {
        [HideInInspector] [SerializeField] protected Image _background;
        [Header("Background")] [SerializeField] private ImageStyle _backgroundStyle = new ImageStyle(new Color(0, 0, 0, .9f));
        public ImageStyle BackgroundStyle => _backgroundStyle;
        
        protected override void Construct()
        {
            base.Construct();
            if (!_background) _background = GetComponent<Image>();
        }

        protected override void ApplyStyle(bool instant)
        {
            base.ApplyStyle(instant);
            if (_background) AddGraphic(_background, BackgroundStyle);
        }
    }
}