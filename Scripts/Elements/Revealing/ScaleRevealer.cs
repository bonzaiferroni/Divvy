﻿using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class ScaleRevealer : ElementRevealer
    {
        public ScaleRevealer(Transform transform, float animationTime, bool easeAnimation) : base(animationTime,
            easeAnimation)
        {
            Transform = transform;
        }

        private Transform Transform { get; }

        public override bool InstantType => false;

        protected override void Modify(float amount)
        {
            Transform.localScale = Vector3.one * amount;
        }
    }
}