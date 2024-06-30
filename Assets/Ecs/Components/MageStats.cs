using Leopotam.EcsLite;
using UnityEngine;

namespace LeoECS
{
    public struct MageStats : IEcsAutoReset<MageStats>
    {
        public int Attack;
        public int Radius;
        public float Reattack;

        public RectTransform CastPoint;
        public RectTransform Position;

        public void AutoReset(ref MageStats c)
        {
            c.Attack = Random.Range(3, 8);
            c.Radius = Random.Range(200, 301);
            c.Reattack = Random.Range(0.5f, 1.5f);
        }
    }
}