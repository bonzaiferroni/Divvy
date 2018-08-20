﻿using UnityEngine;

namespace DivLib.Core
{
    public abstract class DivPosition
    {
        public Vector2 Target { get; protected set; }
        public RectTransform Rect { get; private set; }
        public bool Transported { get; protected set; }
        
        public Vector2 Current
        {
            get { return Rect.anchoredPosition; }
            protected set { Rect.anchoredPosition = value; }
        }

        public abstract void TransportSelf(bool instant);
        
        public void Init(RectTransform rect)
        {
            Rect = rect;
            Transported = false;
        }
        
        public void SetTargetPosition(Vector2 position, bool instant)
        {
            if (position == Target) return;
            Target = position;
				
            if (!instant)
            {
                Transported = false;
            }
            else
            {
                // quick transport
                Current = Target;
                Transported = true;
            }
        }

        public virtual void FinishTransport()
        {
            Current = Target;
            Transported = true;
        }
    }
}