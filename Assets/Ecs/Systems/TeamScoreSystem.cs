using Leopotam.EcsLite;

namespace LeoECS
{
    public class TeamScoreSystem : IEcsRunSystem, IEcsInitSystem
    {
        EcsWorld world;

        EcsFilter _filter;

        EcsPool<ScoreStorage> ScoreStorage;
        EcsPool<Score> scorePool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            _filter = world.Filter<Score>().End();

            ScoreStorage = world.GetPool<ScoreStorage>();
            scorePool = world.GetPool<Score>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref ScoreStorage scoreStorage = ref ScoreStorage.GetRawDenseItems()[0];
                ref Score score = ref scorePool.Get(entity);

                if (score.AddToRedTeam)
                {
                    scoreStorage.RedTeam += score.ScoreCount;
                }
                else
                {
                    scoreStorage.BlueTeam += score.ScoreCount;
                }

                scorePool.Del(entity);
            }
        }
    }
}