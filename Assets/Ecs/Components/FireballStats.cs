using Leopotam.EcsLite;
using UnityEngine;

namespace LeoECS
{
    public struct FireballStats
    {
        public EcsPackedEntity MageOwner;

        public RectTransform Target;
        public RectTransform CurrentRect;

        public float Speed;
    }
}