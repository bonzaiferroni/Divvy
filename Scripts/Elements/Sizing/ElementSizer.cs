using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public abstract class ElementSizer
    {
        protected Vector2 _velocity;
        
        public ElementSizer(RectTransform transform, float animationTime)
        {
            Transform = transform;
            Target = Current;
            AnimationTime = animationTime;
        }

        public float AnimationTime { get; set; }
        public bool EaseAnimation { get; set; }
        public Vector2 Target { get; protected set; }
        public bool Resizing { get; protected set; }
        
        protected RectTransform Transform { get; }
        
        public Vector2 Current
        {
            get => Transform.sizeDelta;
            protected set => Transform.sizeDelta = value;
        }

        public abstract void Refresh(bool instant);
        
        public void SetTargetSize(Vector2 position, bool instant)
        {
            if (position == Target) return;
            Target = position;
            Resizing = true;
				
            if (instant)
            {
                FinishResize();
            }
            else if (!EaseAnimation && AnimationTime > 0)
            {
                _velocity = (Target - Current) / AnimationTime;
            }
        }

        public virtual void FinishResize()
        {
            _velocity = Vector2.zero;
            Current = Target;
            Resizing = false;
        }
    }
}