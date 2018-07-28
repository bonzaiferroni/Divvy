using UnityEngine;

namespace Divvy.Core
{
    [RequireComponent(typeof(DivvyText))]
    public class DivvyFadeText : DivvyAnimatedVisibility
    {
        public DivvyText Label { get; private set; }
        
        public override void Init()
        {
            base.Init();
            Label = GetComponent<DivvyText>();
        }

        protected override void Modify(float amount)
        {
            Label.Alpha = amount;
        }
    }
}