using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class InstantRevealer : ElementRevealer
    {
        public InstantRevealer(DivPosition position) : base(0)
        {
            Position = position;
        }
        
        private DivPosition Position { get; }

        private bool InPosition { get; set; } = true;
        
        public override bool InstantType => true;

        protected override void Modify(float amount)
        {
            if (InPosition && !IsVisible)
            {
                Position.SetTargetPosition(Vector3.up * 10000, true);
                InPosition = false;
            }
            else if (!InPosition && IsVisible)
            {
                Position.FinishTransport();
                InPosition = true;
            }
        }
    }
}