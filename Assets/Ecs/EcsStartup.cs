using Leopotam.EcsLite;
using UnityEditor;
using UnityEngine;

namespace LeoECS
{
    namespace Client
    {
        sealed class EcsStartup : MonoBehaviour
        {
            EcsWorld _world;
            IEcsSystems _systems;

            [SerializeField] private BattleConfig prefabs;

            void Start()
            {
                _world = new EcsWorld();
                _systems = new EcsSystems(_world, prefabs);
                _systems
                    .Add(new MageInitialize())
                    .Add(new MageCastFireball())
                    .Add(new MageCooldown())

                    .Add(new FireballMove())
                    .Add(new FireballExplosion())

                    .Add(new ExplosionRadius())

                    .Add(new TeamScore())
#if UNITY_EDITOR
                    .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                    .Init();
            }

            void Update()
            {
                _systems?.Run();
            }
            void OnDestroy()
            {
                if (_systems != null)
                {
                    _systems.Destroy();
                    _systems = null;
                }

                if (_world != null)
                {
                    _world.Destroy();
                    _world = null;
                }
            }
        }

        public class TeamScore : IEcsRunSystem
        {
            public void Run(IEcsSystems systems)
            {

            }
        }

        public class ExplosionRadius : IEcsRunSystem
        {
            public void Run(IEcsSystems systems)
            {

            }
        }

        public class FireballExplosion : IEcsRunSystem
        {
            public void Run(IEcsSystems systems)
            {

            }
        }

        public class FireballMove : IEcsRunSystem
        {
            public void Run(IEcsSystems systems)
            {

            }
        }

        public class MageCooldown : IEcsRunSystem
        {
            public void Run(IEcsSystems systems)
            {

            }
        }

        public class MageCastFireball : IEcsRunSystem
        {
            public void Run(IEcsSystems systems)
            {

            }
        }
    }
}