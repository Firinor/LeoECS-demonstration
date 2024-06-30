using Leopotam.EcsLite;
using UnityEngine;

namespace LeoECS
{
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        IEcsSystems _systems;

        [SerializeField] private BattleConfig config;

        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world, config);
            _systems
                .Add(new MageInitialize())
                .Add(new MageCastFireball())
                .Add(new MageCooldownSystem())

                .Add(new FireballMove())
                .Add(new FireballExplosion())

                .Add(new ExplosionRadius())

                .Add(new TeamScoreSystem())

                .Add(new UISystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Init();

            Destroy(config);
            config = null;
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
}