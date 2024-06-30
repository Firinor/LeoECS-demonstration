using Leopotam.EcsLite;
using UnityEngine;

namespace LeoECS
{
    public class FireballMove : IEcsRunSystem, IEcsInitSystem
    {
        private static float acceleration = .75f;

        EcsWorld world;

        EcsFilter _filter;

        EcsPool<FireballStats> fireballPool;
        EcsPool<FireballReadyToExplode> fireballToExplodePool;
        EcsPool<Explosion> explosionPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            _filter = world.Filter<FireballStats>().End();

            fireballPool = world.GetPool<FireballStats>();
            fireballToExplodePool = world.GetPool<FireballReadyToExplode>();
            explosionPool = world.GetPool<Explosion>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref FireballStats stats = ref fireballPool.Get(entity);

                stats.Speed += acceleration * Time.deltaTime;

                Vector2 direction = stats.Target.position - stats.CurrentRect.position;

                Vector2 delta = direction.normalized * stats.Speed;

                stats.CurrentRect.anchoredPosition += delta;

                if (direction.magnitude * 100 >= delta.magnitude)
                    continue;

                fireballToExplodePool.Add(entity);
            }
        }
    }
}