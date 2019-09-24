using Bonwerk.Divvy.Elements;
using UnityEngine;

namespace Bonwerk.Divvy
{
    [ExecuteInEditMode]
    public class DivRoot : MonoBehaviour
    {
        [Header("Root Div")] [SerializeField] private Element _div;

        public Element Div
        {
            get => _div;
            set => _div = value;
        }

        private void Awake()
        {
            Init();
        }

        private void Update()
        {
            if (Application.isPlaying)
            {
                Refresh();
            }
            else
            {
                Init();
            }
        }

        public void Init()
        {
            Div = GetComponent<Element>();
            if (!Div) return;
            Div.Init();
            Div.Refresh(true);
        }

        private void Refresh()
        {
            if (!Div) return;
            Div.Refresh(false);
        }
    }
}