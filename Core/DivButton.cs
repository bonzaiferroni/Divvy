using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Core
{
    public class DivButton : Div
    {
        // [SerializeField] private Image _image;
        [SerializeField] private Button _button;

        // public Image Image => _button.image;
        public Button Button => _button;

        public override void Init()
        {
            base.Init();
            _button = GetComponent<Button>();
        }

        public void AddListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }
    }
}