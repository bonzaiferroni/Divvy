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

        private void Update()
        {
            if (Application.isPlaying)
            {
                UpdateWhilePlaying();
            }
            else
            {
                UpdateWhileStopped();
            }
        }

        private void UpdateWhilePlaying()
        {
            if (!Div) return;
            Div.UpdatePosition(false);
        }

        private void UpdateWhileStopped()
        {
            Div = GetComponent<Div>();
            if (!Div) return;
            Div.Init();
            Div.UpdatePosition(true);
        }
    }
}