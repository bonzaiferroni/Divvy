using System;
using UnityEditor;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    [Serializable]
    public abstract class ElementPositioner
    {
        public ElementPositioner(RectTransform transform, float animationTime)
        {
            Transform = transform;
            Target = Current;
            AnimationTime = animationTime;
        }

        public float AnimationTime { get; set; }
        public bool EaseAnimation { get; set; }
        public Vector2 Target { get; protected set; }
        public bool Transporting { get; protected set; }
        
        protected RectTransform Transform { get; }
        
        public Vector2 Current
        {
            get => Transform.anchoredPosition;
            protected set => Transform.anchoredPosition = value;
        }

        public abstract void Refresh(bool instant);
        
        public void SetTargetPosition(Vector2 position, bool instant)
        {
            if (position == Target) return;
            
            Target = position;
            Transporting = true;
				
            if (instant)
            {
                FinishTransport();
            }
        }

        public virtual void FinishTransport()
        {
            Current = Target;
            Transporting = false;
        }
    }
}