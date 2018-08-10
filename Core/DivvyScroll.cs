using UnityEngine;

namespace Divvy.Core
{
    public class DivvyScroll : DivvyParent
    {
        [SerializeField] private DivvyParent _content;
        public Dimensions MaxSize;
        
        public DivvyParent Content => _content;

        public override void Init()
        {
            base.Init();
            Content.Parent = this;
            Content.Init();
            Children.Add(Content);
        }

        public override float Width
        {
            get { return base.Width; }
            set
            {
                if (MaxSize.Width > 0) base.Width = Mathf.Min(MaxSize.Width, value);
                else base.Width = value;
            }
        }
        
        public override float Height
        {
            get { return base.Height; }
            set
            {
                if (MaxSize.Height > 0) base.Height = Mathf.Min(MaxSize.Height, value);
                else base.Height = value;
            }
        }
    }
}