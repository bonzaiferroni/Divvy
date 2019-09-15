using UnityEngine;

namespace Bonwerk.Divvy.Core
{
    [RequireComponent(typeof(DivText))]
    public class DivFadeText : DivAnimatedVisibility
    {
        public DivText Label { get; private set; }
        
        public override void Init()
        {
            Label = GetComponent<DivText>(); // needs to come before base.Init() because Label is referenced
            base.Init();
        }

        protected override void Modify(float amount)
        {
            Label.Alpha = amount;
            Label.RaycastTarget = IsVisible;
        }
    }
}