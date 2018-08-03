using UnityEngine;

namespace Divvy.Core
{
    [RequireComponent(typeof(DivvyText))]
    public class DivvyFadeText : DivvyAnimatedVisibility
    {
        public DivvyText Label { get; private set; }
        
        public override void Init()
        {
            Label = GetComponent<DivvyText>(); // needs to come before base.Init() because Label is referenced
            base.Init();
        }

        protected override void Modify(float amount)
        {
            Label.Alpha = amount;
            Label.RaycastTarget = IsVisible;
        }
    }
}