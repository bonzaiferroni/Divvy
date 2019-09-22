using Bonwerk.Divvy.Data;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class DivScroll : DivElement
    {
        [SerializeField] private DivElement _content;
        public Dimensions MaxSize;
        
        public DivElement Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public override void Init()
        {
            base.Init();
            Content.Parent = this;
            Content.Init();
            Children.Add(Content);
        }
    }
}