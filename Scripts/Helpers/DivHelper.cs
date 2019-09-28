using System;
using Bonwerk.Divvy.Elements;
using UnityEngine;

namespace Bonwerk.Divvy
{
    public static class ElementHelper
    {
        public static IElement GetChild(this Div div, string objectTag)
        {
            foreach (var child in div.Children)
            {
                if (child.Name == objectTag) return child;
            }

            foreach (var child in div.Children)
            {
                var childDiv = child as Div;
                if (childDiv == null) continue;
                var grandChild = childDiv.GetChild(objectTag);
                if (grandChild == null) continue;
                return grandChild;
            }

            return null;
        }

        public static T GetChild<T>(this Div div, string objectTag) where T : class, IElement
        {
            foreach (var child in div.Children)
            {
                if (child.Name == objectTag && child is T) return child as T;
            }

            foreach (var child in div.Children)
            {
                var childDiv = child as Div;
                if (childDiv == null) continue;
                var grandChild = childDiv.GetChild<T>(objectTag);
                if (grandChild == null) continue;
                return grandChild;
            }

            return null;
        }

        public static T GetAndValidate<T>(this Element element, string name) where T : Component
        {
            var components = element.GetComponentsInChildren<T>();
            foreach (var component in components)
            {
                if (component.name == name) return component;
            }
            throw new Exception(
                $"Divvy {element.GetType().Name}: expecting {typeof(T).Name} on GameObject named {name}");
        }
    }
}