using System;
using System.Collections.Generic;
using Divvy.Core;
using FusionLib.Core;
using UnityEngine;

namespace DivLib.Core
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Div))]
    public abstract class DisplayRecipes : MonoBehaviour
    {
        private static DisplayRecipes[] _instances;

        private static DisplayRecipes[] Instances
        {
            get
            {
                if (_instances == null) _instances = FindObjectsOfType<DisplayRecipes>();
                return _instances;
            }
        }

        private Div _div;
        private List<GameObject> _gameObjects = new List<GameObject>();
        public abstract List<Fusion> Recipes { get; }

        private void Start()
        {
            _div = GetComponent<Div>();
            _div.Style = LayoutStyle.Vertical;
        }
        
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            // if (!Application.isFocused) return;
            foreach (var instance in Instances)
            {
                instance.Reload();
            }
        }

        [ContextMenu("Reload")]
        public void Reload()
        {
            Clear();
            Display();
        }

        private void Clear()
        {
            foreach (var panel in _div.Children.ToArray())
            {
                _div.RemoveChild(panel);
            }

            foreach (var go in _gameObjects)
            {
                DestroyImmediate(go);
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                var tran = transform.GetChild(i);
                DestroyImmediate(tran.gameObject);
            }

            _gameObjects.Clear();
        }

        private void Display()
        {
            foreach (var recipe in Recipes)
            {
                _gameObjects.Add(recipe.Go);
                try
                {
                    var element = recipe.Get<Element>();
                    _div.AddChild(element);
                    element.Init();
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
        }
    }
}