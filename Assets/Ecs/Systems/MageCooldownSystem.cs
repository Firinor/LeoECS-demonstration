using Leopotam.EcsLite;
using UnityEngine;

namespace LeoECS
{
    public class MageCooldownSystem : IEcsRunSystem, IEcsInitSystem
    {
        EcsFilter _filter;
        EcsPool<MageCooldown> mageCooldownPool;
        EcsPool<MageReadyToFire> mageReadyToFire;
        EcsPool<MageStats> mageStats;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            _filter = world.Filter<MageCooldown>().End();

            mageCooldownPool = world.GetPool<MageCooldown>();
            mageReadyToFire = world.GetPool<MageReadyToFire>();
            mageStats = world.GetPool<MageStats>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref MageCooldown weapon = ref mageCooldownPool.Get(entity);

                if(weapon.CurrentCooldown <= 0)
                {
                    mageReadyToFire.Add(entity);
                    weapon.CurrentCooldown += mageStats.Get(entity).Reattack;
                }
                else
                    weapon.CurrentCooldown -= Time.deltaTime;
            }
        }
    }
}