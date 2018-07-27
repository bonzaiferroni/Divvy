using Divvy.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Divvy.Tests
{
    public class DivvyData
    {
        public DivvyData(int subCount = 3)
        {
            Canvas = new GameObject("Canvas", typeof(Canvas));
            Canvas.AddComponent<CanvasScaler>();
            RootObject = new GameObject("VerticalParent", typeof(RectTransform));
            RootObject.transform.SetParent(Canvas.transform);
            StandardizeRect(RootObject);
            RootParent = RootObject.AddComponent<DivvyParent>();
            for (var i = 0; i < subCount; i++)
            {
                var horizontalParentGo = new GameObject("HorizontalParent_" + i, typeof(RectTransform));
                StandardizeRect(horizontalParentGo);
                horizontalParentGo.AddComponent<DivvyParent>().Style = LayoutStyle.Horizontal;
                horizontalParentGo.transform.SetParent(RootObject.transform);
                for (var j = 0; j < subCount; j++)
                {
                    var layoutGo = new GameObject($"Child_{i}_{j}", typeof(RectTransform));
                    StandardizeRect(layoutGo);
                    layoutGo.AddComponent<DivvyPanel>();
                    layoutGo.AddComponent<DivvyScale>();
                    layoutGo.AddComponent<Image>().color = Random.ColorHSV();
                    layoutGo.transform.SetParent(horizontalParentGo.transform);
                }
            }

            RootParent.Init();
        }

        private void StandardizeRect(GameObject rootObject)
        {
            var rect = rootObject.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(0, 1);
            rect.pivot = new Vector2(0, 1);
        }

        public DivvyParent RootParent { get; }
        public GameObject RootObject { get; }
        public GameObject Canvas { get; }
    }
}