using Leopotam.EcsLite;

class ButtonPressSystem : IEcsRunSystem
{
    public void Run(EcsSystems systems)
    {
        _openDoors = false;
        DoorOpen(systems);
        DoorReset(systems);
    }

    private void DoorOpen(EcsSystems systems)
    {
        var filter = systems.GetWorld().Filter<ButtonComponent>().Exc<ResetButtonTag>().End();
        var poolButton = systems.GetWorld().GetPool<ButtonComponent>();
        var elevatorEffectPool = systems.GetWorld().GetPool<ElevatorEffectComponent>();
        var poolDoor = systems.GetWorld().GetPool<DoorComponent>();
        var poolElevatorEffect = systems.GetWorld().GetPool<ElevatorEffectComponent>();
        foreach (int i in filter)
        {
            ref var button = ref poolButton.Get(i);
            ref var elevatorEffect = ref elevatorEffectPool.Get(i);
            ref var door = ref poolDoor.Get((int)button.DoorConvertToEntity.TryGetEntity());
            ref var elevatorEffectDoor = ref poolElevatorEffect.Get((int)button.DoorConvertToEntity.TryGetEntity());

            if (button.IsPressed == elevatorEffect.IsUp)
            {
                elevatorEffect.IsUp = !button.IsPressed;
                elevatorEffectDoor.IsUp = !button.IsPressed;
            }

            if (button.IsPressed != door.IsOpen)
            {
                door.IsOpen = button.IsPressed;
                elevatorEffectDoor.Update = button.IsPressed;
            }
        }
    }

    private bool _openDoors;
    private void DoorReset(EcsSystems systems)
    {
        var poolButton = systems.GetWorld().GetPool<ButtonComponent>();

        var buttonFfilter = systems.GetWorld().Filter<ButtonComponent>().Inc<ResetButtonTag>().End();
        var elevatorEffect = systems.GetWorld().GetPool<ElevatorEffectComponent>();

        foreach (int i in buttonFfilter)
        {
            ref var button = ref poolButton.Get(i);
            ref var elevatorEffectDoor = ref elevatorEffect.Get(i);
            if (button.IsPressed == elevatorEffectDoor.IsUp)
            {
                elevatorEffectDoor.IsUp = !button.IsPressed;
            }
            if (button.IsPressed) _openDoors = true;

        }

        if (!_openDoors) return;
        var doorFilter = systems.GetWorld().Filter<DoorComponent>().End();
        var poolElevatorEffect = systems.GetWorld().GetPool<ElevatorEffectComponent>();
        var poolDoor = systems.GetWorld().GetPool<DoorComponent>();
        foreach (int i in doorFilter)
        {
            ref var door = ref poolDoor.Get(i);
            ref var elevatorEffectDoor = ref poolElevatorEffect.Get(i);

            elevatorEffectDoor.IsUp = true;
            door.IsOpen = false;
            elevatorEffectDoor.Update = true;

        }
    }
}