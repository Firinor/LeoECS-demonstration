using Leopotam.EcsLite;
using UnityEngine;

namespace LeoECS
{
    public class MageInitialize : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            BattleConfig configs = systems.GetShared<BattleConfig>();

            for (int i = 0; i < configs.MageCount; i++)
            {
                RectTransform blueMage = GameObject.Instantiate(configs.BlueMagePrefab, configs.blueTeam);
                float randomX = Random.Range(0, configs.blueTeam.rect.width);
                float randomY = Random.Range(0, configs.blueTeam.rect.height);
                blueMage.GetComponent<RectTransform>().anchoredPosition = new Vector2(randomX, randomY);

                RectTransform redMage = GameObject.Instantiate(configs.RedMagePrefab, configs.redTeam);
                randomX = Random.Range(0, configs.redTeam.rect.width);
                randomY = Random.Range(0, configs.redTeam.rect.height);
                redMage.GetComponent<RectTransform>().anchoredPosition = new Vector2(randomX, randomY);
            }
        }
    }
}