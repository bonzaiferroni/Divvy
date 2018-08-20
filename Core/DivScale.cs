using UnityEngine;

namespace DivLib.Core
{
    public class DivScale : DivAnimatedVisibility
    {
        protected override void Modify(float amount)
        {
            transform.localScale = Vector3.one * amount;
        }
    }
}