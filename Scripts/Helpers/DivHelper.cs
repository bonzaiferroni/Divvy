using System;
using Bonwerk.Divvy.Elements;
using UnityEngine;

namespace Bonwerk.Divvy.Helpers
{
    public static class ElementHelper
    {
        public static IElement GetChild(this DivElement div, string objectTag)
        {
            foreach (var child in div.Children)
            {
                if (child.Name == objectTag) return child;
            }

            foreach (var child in div.Children)
            {
                var childDiv = child as DivElement;
                if (childDiv == null) continue;
                var grandChild = childDiv.GetChild(objectTag);
                if (grandChild == null) continue;
                return grandChild;
            }

            return null;
        }

        public static T GetChild<T>(this DivElement div, string objectTag) where T : class, IElement
        {
            foreach (var child in div.Children)
            {
                if (child.Name == objectTag && child is T) return child as T;
            }

            foreach (var child in div.Children)
            {
                var childDiv = child as DivElement;
                if (childDiv == null) continue;
                var grandChild = childDiv.GetChild<T>(objectTag);
                if (grandChild == null) continue;
                return grandChild;
            }

            return null;
        }

        public static T GetAndValidate<T>(this Element element, string name) where T : MonoBehaviour
        {
            var component = element.GetComponentInChildren<T>();
            if (component.name != name)
                throw new Exception(
                    $"Divvy initialization failed, found {typeof(T).Name} on GameObject named {component.name}, was expecting {name}");
            return component;
        }
    }
}