using UnityEngine;

namespace Bonwerk.Divvy.Visibility
{
    public class ElementScale : AnimatedVisibility
    {
        protected override void Modify(float amount)
        {
            transform.localScale = Vector3.one * amount;
        }
    }
}