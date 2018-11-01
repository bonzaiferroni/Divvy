using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DivLib.Core
{
    public class DivButton : Div
    {
        // [SerializeField] private Image _image;
        [SerializeField] private Button _button;

        // public Image Image => _button.image;
        public Button Button => _button;

        internal override void Init()
        {
            // _image = GetComponent<Image>();
            _button = GetComponent<Button>();
            base.Init();
        }

        public void AddListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }
    }
}