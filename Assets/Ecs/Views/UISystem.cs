using Leopotam.EcsLite;
using TMPro;

namespace LeoECS
{
    public class UISystem : IEcsInitSystem, IEcsRunSystem
    {
        TextMeshProUGUI BlueTeamScoreUI;
        TextMeshProUGUI RedTeamScoreUI;

        EcsPool<ScoreStorage> pool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            BattleConfig configs = systems.GetShared<BattleConfig>();
            BlueTeamScoreUI = configs.BlueTeamScoreUI;
            RedTeamScoreUI = configs.RedTeamScoreUI;

            pool = world.GetPool<ScoreStorage>();
        }

        public void Run(IEcsSystems systems)
        {
            ref ScoreStorage storage = ref pool.GetRawDenseItems()[0];

            BlueTeamScoreUI.text = $"Score: {storage.BlueTeam}";
            RedTeamScoreUI.text = $"Score: {storage.RedTeam}";
        }
    }
}