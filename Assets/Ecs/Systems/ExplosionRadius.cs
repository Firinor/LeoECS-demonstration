using Leopotam.EcsLite;
using UnityEngine;

namespace LeoECS
{
    public class ExplosionRadius : IEcsRunSystem, IEcsInitSystem
    {
        public static float explosionSpeed = 1350f;

        EcsWorld world;

        EcsFilter _filter;

        EcsPool<Explosion> explosionPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            _filter = world.Filter<Explosion>().End();

            explosionPool = world.GetPool<Explosion>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref Explosion e = ref explosionPool.Get(entity);

                if (e.isExpansion)
                {
                    e.currentRadius += explosionSpeed * Time.deltaTime;
                    if (e.currentRadius >= e.Radius)
                        e.isExpansion = false;
                }
                else
                {
                    e.currentRadius -= explosionSpeed * Time.deltaTime;
                    if (e.currentRadius <= 0)
                    {
                        GameObject.Destroy(e.RectTransform.gameObject);
                        world.DelEntity(entity);
                    }
                }

                e.RectTransform.sizeDelta = new Vector2(e.currentRadius, e.currentRadius);
            }
        }
    }
}