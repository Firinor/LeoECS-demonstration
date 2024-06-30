using Leopotam.EcsLite;
using UnityEngine;

namespace LeoECS
{
    public struct Explosion: IEcsAutoReset<Explosion>
    {
        public float Radius;
        public RectTransform RectTransform;
        public float currentRadius;
        public bool isExpansion;

        public void AutoReset(ref Explosion c)
        {
            c.isExpansion = true;
        }
    }
}