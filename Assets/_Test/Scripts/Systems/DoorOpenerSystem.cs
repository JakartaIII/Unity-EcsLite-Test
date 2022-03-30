using Leopotam.EcsLite;

public class DoorOpenerSystem : IEcsRunSystem
{
    public void Run(EcsSystems systems)
    {
        var filter = systems.GetWorld().Filter<DoorComponent>().Inc<ElevatorEffectComponent>().End();
        var poolDoor = systems.GetWorld().GetPool<DoorComponent>();
        var poolElevatorEffect = systems.GetWorld().GetPool<ElevatorEffectComponent>();
        foreach (int i in filter)
        {
            ref var door = ref poolDoor.Get(i);
            ref var elevatorEffect = ref poolElevatorEffect.Get(i);
              
            if (door.IsOpen != elevatorEffect.IsUp)
            {
                elevatorEffect.IsUp = door.IsOpen;
                elevatorEffect.Update = elevatorEffect.IsUp;
            }
        }
    }
}