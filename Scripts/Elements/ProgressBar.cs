using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class ProgressBar : ProgressElement
    {
        protected override void DisplayFill(float value)
        {
            _progressRect.sizeDelta = new Vector2(ContentSize.x * value, ContentSize.y);
        }
    }
}