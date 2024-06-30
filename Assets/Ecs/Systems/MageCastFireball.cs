using Leopotam.EcsLite;
using UnityEngine;

namespace LeoECS
{
    public class MageCastFireball : IEcsRunSystem, IEcsInitSystem
    {
        EcsWorld world;

        EcsFilter _filterMageReadyToFire;
        EcsFilter _filterRedTeam;
        EcsFilter _filterBlueTeam;

        EcsPool<FireballStats> FireballStatsPool;

        EcsPool<MageReadyToFire> mageReadyToFire;
        EcsPool<MageStats> mageStatsPool;

        EcsPool<RedTeam> RedTeamPool;
        EcsPool<BlueTeam> BlueTeamPool;

        RectTransform FireballPrefab;
        RectTransform BulletParent;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            BattleConfig configs = systems.GetShared<BattleConfig>();

            FireballPrefab = configs.FireballPrefab;
            BulletParent = configs.BulletsParent;

            _filterMageReadyToFire = world.Filter<MageReadyToFire>().End();
            _filterRedTeam = world.Filter<RedTeam>().End();
            _filterBlueTeam = world.Filter<BlueTeam>().End();

            mageReadyToFire = world.GetPool<MageReadyToFire>();
            mageStatsPool = world.GetPool<MageStats>();

            FireballStatsPool = world.GetPool<FireballStats>();

            RedTeamPool = world.GetPool<RedTeam>();
            BlueTeamPool = world.GetPool<BlueTeam>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filterMageReadyToFire)
            {
                mageReadyToFire.Del(entity);

                RectTransform fireball = GameObject.Instantiate(
                    FireballPrefab, mageStatsPool.Get(entity).CastPoint.position, Quaternion.identity, BulletParent);

                int FireballEntity = world.NewEntity();
                ref FireballStats stats = ref FireballStatsPool.Add(FireballEntity);

                EcsPackedEntity packedOwner = world.PackEntity(entity);
                stats.MageOwner = packedOwner;
                stats.CurrentRect = fireball;
                stats.Target = FindEnemy(entity);
            }
        }
        private RectTransform FindEnemy(int entity)
        {
            int targetIndex = -1; 
            int enemyEntity = -1;

            if (RedTeamPool.Has(entity))
            {
                targetIndex = Random.Range(0, _filterBlueTeam.GetEntitiesCount());
                enemyEntity = _filterBlueTeam.GetRawEntities()[targetIndex];
            }
            else if (BlueTeamPool.Has(entity))
            {
                targetIndex = Random.Range(0, _filterRedTeam.GetEntitiesCount());
                enemyEntity = _filterRedTeam.GetRawEntities()[targetIndex];
            }
            return mageStatsPool.Get(enemyEntity).Position;
        }
    }

}