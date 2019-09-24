using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public abstract class BackgroundElement : Element
    {
        [HideInInspector] [SerializeField] protected Image _background;
        [Header("Background")] [SerializeField] private ImageStyle _backgroundStyle = new ImageStyle(new Color(0, 0, 0, .9f));
        public ImageStyle BackgroundStyle => _backgroundStyle;
        
        public override void Init()
        {
            base.Init();
            if (!_background) _background = GetComponent<Image>();
            if (_background) ApplyStyles.Image(_background, BackgroundStyle);
        }
    }
}