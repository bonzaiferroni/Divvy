using System;
using UnityEngine;

namespace Bonwerk.Divvy.Positioning
{
    [Serializable]
    public abstract class DivPosition
    {
        public DivPosition(RectTransform transform)
        {
            Transform = transform;
        }
        
        public Vector2 Target { get; protected set; }
        public bool Transporting { get; protected set; }
        
        protected RectTransform Transform { get; }
        
        public Vector2 Current
        {
            get { return Transform.anchoredPosition; }
            protected set { Transform.anchoredPosition = value; }
        }

        public abstract void Refresh(bool instant);
        
        public void SetTargetPosition(Vector2 position, bool instant)
        {
            if (!instant && position == Target) return;
            Target = position;
				
            if (!instant)
            {
                Transporting = false;
            }
            else
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