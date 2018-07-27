using UnityEngine;

namespace Divvy.Core
{
    public class DivvyScale : DivvyVisibility
    {
        protected override void Modify(float amount)
        {
            transform.localScale = Vector3.one * amount;
        }
    }
}