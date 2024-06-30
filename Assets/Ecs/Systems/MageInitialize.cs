using Leopotam.EcsLite;
using UnityEngine;

namespace LeoECS
{
    public class MageInitialize : IEcsInitSystem
    {
        EcsWorld world;
        BattleConfig configs;

        EcsPool<MageStats> MageStatsPool;
        EcsPool<MageCooldown> MageCooldownPool;

        EcsPool<RedTeam> RedTeamPool;
        EcsPool<BlueTeam> BlueTeamPool;

        public void Init(IEcsSystems systems)
        {
            configs = systems.GetShared<BattleConfig>();
            world = systems.GetWorld();

            MageStatsPool =  world.GetPool<MageStats>();
            MageCooldownPool = world.GetPool<MageCooldown>();

            RedTeamPool = world.GetPool<RedTeam>();
            BlueTeamPool = world.GetPool<BlueTeam>();

            for (int i = 0; i < configs.MageCount; i++)
            {
                CreateBlueMage();
                CreateRedMage();
            }
        }

        private void CreateRedMage()
        {
            RectTransform redMage = GameObject.Instantiate(configs.RedMagePrefab, configs.redTeam);
            float randomX = Random.Range(0, configs.redTeam.rect.width);
            float randomY = Random.Range(0, configs.redTeam.rect.height);
            redMage.anchoredPosition = new Vector2(randomX, randomY);

            int entity = world.NewEntity();
            ref MageStats stats = ref MageStatsPool.Add(entity);
            stats.CastPoint = redMage.GetChild(0).GetComponent<RectTransform>();
            stats.Position = redMage;
            MageCooldownPool.Add(entity);
            RedTeamPool.Add(entity);
        }
        private void CreateBlueMage()
        {
            RectTransform blueMage = GameObject.Instantiate(configs.BlueMagePrefab, configs.blueTeam);
            float randomX = Random.Range(0, configs.blueTeam.rect.width);
            float randomY = Random.Range(0, configs.blueTeam.rect.height);
            blueMage.anchoredPosition = new Vector2(randomX, randomY);

            int entity = world.NewEntity();
            ref MageStats stats = ref MageStatsPool.Add(entity);
            stats.CastPoint = blueMage.GetChild(0).GetComponent<RectTransform>();
            stats.Position = blueMage;
            MageCooldownPool.Add(entity);
            BlueTeamPool.Add(entity);
        }
    }

    public struct BlueTeam
    {
    }

    public struct RedTeam
    {
    }
}