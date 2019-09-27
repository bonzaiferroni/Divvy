using Bonwerk.Divvy.Elements;
using UnityEngine;

namespace Bonwerk.Divvy
{
    [ExecuteInEditMode]
    public class DocumentRoot : MonoBehaviour
    {
        [Header("Root Element")] [SerializeField] private Element _element;

        public Element Element
        {
            get => _element;
            set => _element = value;
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
            Element = GetComponent<Element>();
            if (!Element) return;
            Element.Init();
            Element.Refresh(true);
        }

        private void Refresh()
        {
            if (!Element) return;
            Element.Refresh(false);
        }
    }
}