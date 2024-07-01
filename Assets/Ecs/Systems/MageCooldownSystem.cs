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
                ref MageCooldown cooldown = ref mageCooldownPool.Get(entity);

                if(cooldown.CurrentCooldown <= 0)
                {
                    mageReadyToFire.Add(entity);
                    cooldown.CurrentCooldown += mageStats.Get(entity).Reattack;
                }
                else
                    cooldown.CurrentCooldown -= Time.deltaTime;
            }
        }
    }
}