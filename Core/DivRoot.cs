using UnityEngine;

namespace Bonwerk.Divvy.Core
{
    [ExecuteInEditMode]
    public class DivRoot : MonoBehaviour
    {
        [Header("Root Div")] [SerializeField] private Div _div;

        public Div Div
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
                UpdateWhilePlaying();
            }
            else
            {
                Init();
                UpdateWhileStopped();
            }
        }

        public void Init()
        {
            Div = GetComponent<Div>();
            if (!Div) return;
            Div.Init();
        }

        private void UpdateWhilePlaying()
        {
            if (!Div) return;
            Div.UpdatePosition(false);
        }

        private void UpdateWhileStopped()
        {
            
            if (!Div) return;
            Div.UpdatePosition(true);
        }
    }
}