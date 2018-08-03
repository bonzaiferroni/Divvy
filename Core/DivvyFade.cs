using UnityEngine;

namespace Divvy.Core
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DivvyFade : DivvyAnimatedVisibility
    {
        private CanvasGroup _canvasGroup;

        public override void Init()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            base.Init();
        }

        protected override void Modify(float amount)
        {
            _canvasGroup.alpha = amount;
            if (IsVisible)
            {
                if (!_canvasGroup.blocksRaycasts) _canvasGroup.blocksRaycasts = true;
            }
            else
            {
                if (_canvasGroup.blocksRaycasts) _canvasGroup.blocksRaycasts = false;
            }
        }
    }
}