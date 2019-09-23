using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public abstract class ElementSizer
    {
        public ElementSizer(RectTransform transform)
        {
            Transform = transform;
            Target = Current;
        }
        
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
				
            if (!instant)
            {
                Resizing = true;
            }
            else
            {
                FinishResize();
            }
        }

        public virtual void FinishResize()
        {
            Current = Target;
            Resizing = false;
        }
    }
}