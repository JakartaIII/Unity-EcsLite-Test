using LeoEcsPhysics;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

sealed class EcsStartup : MonoBehaviour
{
    EcsSystems _systems;
    private EcsSystems _ecsSystems;

    void Start()
    {
        var world = new EcsWorld();
        _systems = new EcsSystems(world);
        _systems
            .Add(new PlayerPointAndClickSystem())
            .Add(new ButtonPressSystem())
            .Add(new ElevatorEffectSystem())
            .Add(new GameQuitSystem())

#if UNITY_EDITOR 
            .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif
        _systems.ConvertScene();
        _systems.Init();

        _ecsSystems = new EcsSystems(world);
        EcsPhysicsEvents.ecsWorld = world;
        _ecsSystems
            .Add(new ButtonPressTriggerSystem())
            .DelHerePhysics();
        _ecsSystems.Init();
    }

    void Update()
    {
        _systems?.Run();
    }

    private void FixedUpdate()
    {
        _ecsSystems?.Run();
    }

    void OnDestroy()
    {
        if (_systems != null)
        {
            _systems.Destroy();
            _systems.GetWorld().Destroy();
            _systems = null;
        }

        if (_ecsSystems != null)
        {
            EcsPhysicsEvents.ecsWorld = null;
            _ecsSystems.Destroy();
            _ecsSystems = null;
        }
    }
}