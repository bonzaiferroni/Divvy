using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class ProgressCircle : ProgressElement
    {
        protected override void DisplayFill(float value)
        {
            _progress.fillAmount = value;
        }
    }
}