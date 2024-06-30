using Leopotam.EcsLite;
using UnityEngine;

namespace LeoECS
{
    public class FireballExplosion : IEcsRunSystem, IEcsInitSystem
    {
        RectTransform ExplosionPrefab;
        RectTransform BulletParent;

        EcsWorld world;

        EcsFilter _filter;

        EcsFilter _filterRedTeam;
        EcsFilter _filterBlueTeam;

        EcsPool<FireballStats> fireballPool;
        EcsPool<FireballReadyToExplode> fireballToExplodePool;
        EcsPool<Explosion> explosionPool;
        EcsPool<MageStats> mageStatsPool;

        EcsPool<Score> ScorePool;

        EcsPool<RedTeam> RedTeamPool;
        EcsPool<BlueTeam> BlueTeamPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            _filter = world.Filter<FireballReadyToExplode>().End();

            _filterRedTeam = world.Filter<RedTeam>().End();
            _filterBlueTeam = world.Filter<BlueTeam>().End();

            fireballToExplodePool = world.GetPool<FireballReadyToExplode>();
            explosionPool = world.GetPool<Explosion>();
            mageStatsPool = world.GetPool<MageStats>();
            fireballPool = world.GetPool<FireballStats>();

            ScorePool = world.GetPool<Score>();

            RedTeamPool = world.GetPool<RedTeam>();
            BlueTeamPool = world.GetPool<BlueTeam>();

            BattleConfig configs = systems.GetShared<BattleConfig>();
            ExplosionPrefab = configs.ExplosionPrefab;
            BulletParent = configs.BulletsParent;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                int ExplosionEntity = world.NewEntity();
                ref Explosion stats = ref explosionPool.Add(ExplosionEntity);
                ref FireballStats fireball = ref fireballPool.Get(entity);

                RectTransform explosionTransform = GameObject.Instantiate(
                    ExplosionPrefab,
                    fireball.Target.position, 
                    Quaternion.identity,
                    BulletParent);

                stats.RectTransform = explosionTransform;
                if (fireball.MageOwner.Unpack(world, out int mageEntity))
                {
                    MageStats mage = mageStatsPool.Get(mageEntity);
                    stats.Radius = mage.Radius;

                    int mageAttack = mage.Attack;

                    int ScoreEntity = world.NewEntity();

                    if (RedTeamPool.Has(mageEntity))
                    {
                        int enemyesOnAttack = FindEnemy(mage);
                        ref Score score = ref ScorePool.Add(ScoreEntity);
                        score.AddToRedTeam = true;
                        score.ScoreCount = mageAttack * enemyesOnAttack;
                    }
                    else //BlueTeam
                    {
                        int enemyesOnAttack = FindEnemy(mage);
                        ref Score score = ref ScorePool.Add(ScoreEntity);
                        score.AddToRedTeam = false;
                        score.ScoreCount = mageAttack * enemyesOnAttack;
                    }
                }

                GameObject.Destroy(fireballPool.Get(entity).CurrentRect.gameObject);
                world.DelEntity(entity);
            }
        }

        private int FindEnemy(MageStats attacker)
        {
            int result = 0;

            for (int i = 0; i < RedTeamPool.GetRawDenseItemsCount(); i++)
            {

                MageStats mage = mageStatsPool.Get(_filterRedTeam.GetRawEntities()[i]);

                RectTransform targetMage = (RectTransform)mage.Position;

                if (Vector2.Distance(targetMage.anchoredPosition, attacker.Position.anchoredPosition) <= attacker.Radius)
                    result++;
            }

            return result;
        }
    }
}