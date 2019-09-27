using Bonwerk.Divvy;
using Bonwerk.Divvy.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace DivLib.Tests
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
            RootParent = RootObject.AddComponent<Div>();
            RootObject.AddComponent<DocumentRoot>();
            for (var i = 0; i < subCount; i++)
            {
                var horizontalParentGo = new GameObject("HorizontalParent_" + i, typeof(RectTransform));
                StandardizeRect(horizontalParentGo);
                horizontalParentGo.AddComponent<Div>();
                horizontalParentGo.transform.SetParent(RootObject.transform);
                for (var j = 0; j < subCount; j++)
                {
                    var layoutGo = new GameObject($"Child_{i}_{j}", typeof(RectTransform));
                    StandardizeRect(layoutGo);
                    layoutGo.AddComponent<Element>();
                    layoutGo.AddComponent<Image>().color = Random.ColorHSV();
                    layoutGo.transform.SetParent(horizontalParentGo.transform);
                }
            }
        }

        private void StandardizeRect(GameObject rootObject)
        {
            var rect = rootObject.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(0, 1);
            rect.pivot = new Vector2(0, 1);
        }

        public Div RootParent { get; }
        public GameObject RootObject { get; }
        public GameObject Canvas { get; }
    }
}