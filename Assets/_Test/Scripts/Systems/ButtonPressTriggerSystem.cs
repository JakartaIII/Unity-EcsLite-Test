using LeoEcsPhysics;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

public class ButtonPressTriggerSystem : IEcsRunSystem
{
    public void Run(EcsSystems ecsSystems)
    {
        DoorButton(ecsSystems); 
    }

    private void DoorButton(EcsSystems ecsSystems)
    {
        var filterEnter = ecsSystems.GetWorld().Filter<OnTriggerEnterEvent>().End();
        var poolEnter = ecsSystems.GetWorld().GetPool<OnTriggerEnterEvent>();


        var filterExit = ecsSystems.GetWorld().Filter<OnTriggerExitEvent>().End();
        var poolExit = ecsSystems.GetWorld().GetPool<OnTriggerExitEvent>();

        var poolButton = ecsSystems.GetWorld().GetPool<ButtonComponent>();

        foreach (var i in filterEnter)
        {
            ref var enter = ref poolEnter.Get(i);
            int tryGetEntity = (int) enter.senderGameObject.GetComponent<ConvertToEntity>().TryGetEntity();
            ref var button = ref poolButton.Get(tryGetEntity);
            button.IsPressed = true;
        }

        foreach (var i in filterExit)
        {
            ref var exit = ref poolExit.Get(i);
            int tryGetEntity = (int) exit.senderGameObject.GetComponent<ConvertToEntity>().TryGetEntity();
            ref var button = ref poolButton.Get(tryGetEntity);
            button.IsPressed = false;
        }
    }

     
}