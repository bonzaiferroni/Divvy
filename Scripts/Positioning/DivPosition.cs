﻿using System;
using UnityEngine;

namespace Bonwerk.Divvy.Positioning
{
    [Serializable]
    public abstract class DivPosition
    {
        public DivPosition(RectTransform rect)
        {
            Rect = rect;
        }
        
        public Vector2 Target { get; protected set; }
        public RectTransform Rect { get; private set; }
        public bool Transported { get; protected set; }
        
        public Vector2 Current
        {
            get { return Rect.anchoredPosition; }
            protected set { Rect.anchoredPosition = value; }
        }

        public abstract void Refresh(bool instant);
        
        public void SetTargetPosition(Vector2 position, bool instant)
        {
            if (!instant && position == Target) return;
            Target = position;
				
            if (!instant)
            {
                Transported = false;
            }
            else
            {
                FinishTransport();
            }
        }

        public virtual void FinishTransport()
        {
            Current = Target;
            Transported = true;
        }
    }
}