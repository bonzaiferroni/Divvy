﻿using UnityEngine;

namespace DivLib.Core
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DivInstantFade : DivInstantVisibility
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public override void Init()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            base.Init();
        }

        protected override void Modify(bool isVisible)
        {
            _canvasGroup.alpha = isVisible ? 1 : 0;
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