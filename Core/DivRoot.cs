using UnityEngine;

namespace DivLib.Core
{
    [ExecuteInEditMode]
    public class DivRoot : MonoBehaviour
    {
        [Header("Root Div")] public Div Div;

        public int ChildCount;

        public bool[] ChildVisibility;
        
        public bool Initialized { get; private set; }
        
        private void Awake()
        {
            if (Application.isPlaying)
            {
                Initialize();
            }
            else
            {
                if (Div == null) Div = GetComponent<Div>();
                Initialize();
            }
        }
        
        private void Update()
        {
            if (Div == null) return;

            if (Application.isPlaying)
            {
                Div.UpdatePosition(false);
            }
            else
            {
                EditorUpdate();
            }
        }

        public void Initialize()
        {
            if (Initialized) return;
            Initialized = true;
            if (Div == null) Div = GetComponent<Div>();
            Div.Init();
            Div.UpdatePosition(true);

            if (ChildVisibility == null || Div.Children.Count != ChildVisibility.Length) return;

            for (int i = 0; i < Div.Children.Count; i++)
            {
                var visible = ChildVisibility[i];
                if (visible) continue;
                var child = Div.Children[i];
                child.Visibility.SetVisibility(false, true);
            }
        }

        private void EditorUpdate()
        {
            Div.Init();
            Div.UpdatePosition(true);

            if (Div.Children.Count == ChildCount) return;
            ChildCount = Div.Children.Count;

            var previous = ChildVisibility;

            ChildVisibility = new bool[ChildCount];

            for (int i = 0; i < ChildCount; i++)
            {
                if (previous != null && i < previous.Length)
                {
                    ChildVisibility[i] = previous[i];
                }
                else
                {
                    ChildVisibility[i] = true;
                }
            }
        }
    }
}