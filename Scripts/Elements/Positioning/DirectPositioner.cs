﻿using System;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    [Serializable]
    public class DirectPositioner : ElementPositioner
    {
        private Vector2 _velocity;
        
        public DirectPositioner(RectTransform transform) : base(transform)
        {
        }
        
        public override void Refresh(bool instant)
        {
            if (!instant && (Current - Target).sqrMagnitude > .001f)
            {
                Current = Vector2.SmoothDamp(Current, Target, ref _velocity, .2f);
                return;
            }

            FinishTransport();
        }

        public override void FinishTransport()
        {
            base.FinishTransport();
            _velocity = Vector2.zero;
        }
    }
}