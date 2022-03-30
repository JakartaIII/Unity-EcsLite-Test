using System;
using Leopotam.EcsLite;
using UnityEngine;

sealed class ElevatorEffectSystem : IEcsRunSystem, IEcsInitSystem
{

    public void Init(EcsSystems systems)
    {
        var filter = systems.GetWorld().Filter<ElevatorEffectComponent>().End();
        var poolElevatorEffect = systems.GetWorld().GetPool<ElevatorEffectComponent>();
        foreach (int i in filter)
        {
            ref var elevatorEffect = ref poolElevatorEffect.Get(i);
            elevatorEffect.TransformCol.localPosition = new Vector3(elevatorEffect.TransformCol.localPosition.x, elevatorEffect.CurrentHeight, elevatorEffect.TransformCol.localPosition.z);
        }
    }

    public void Run(EcsSystems systems)
    {
        var filter = systems.GetWorld().Filter<ElevatorEffectComponent>().End();

        var poolElevatorEffect = systems.GetWorld().GetPool<ElevatorEffectComponent>();
        foreach (int i in filter)
        {
            ref var elevatorEffect = ref poolElevatorEffect.Get(i);
            float heigh = Mathf.Abs(elevatorEffect.MaxHeight - elevatorEffect.MinHeight);
            if (!elevatorEffect.Update) continue;
            if (elevatorEffect.IsUp)
            {
                if (elevatorEffect.CurrentHeight < elevatorEffect.MaxHeight)
                {
                    elevatorEffect.CurrentHeight +=  heigh * Time.deltaTime / elevatorEffect.SecondsToLift;
                }
                if (elevatorEffect.CurrentHeight > elevatorEffect.MaxHeight)
                {
                    elevatorEffect.CurrentHeight = elevatorEffect.MaxHeight;
                }
            }
            else
            {
                if (elevatorEffect.CurrentHeight > elevatorEffect.MinHeight)
                {
                    elevatorEffect.CurrentHeight -= heigh * Time.deltaTime / elevatorEffect.SecondsToLift;
                }
                if (elevatorEffect.CurrentHeight < elevatorEffect.MinHeight)
                {
                    elevatorEffect.CurrentHeight = elevatorEffect.MinHeight;
                }
            }

            var elevatorEffectTransformCol = elevatorEffect.TransformCol;
            if (Math.Abs(elevatorEffectTransformCol.localPosition.y - elevatorEffect.CurrentHeight) > 0.0001f)
            {
                elevatorEffectTransformCol.localPosition = new Vector3(elevatorEffectTransformCol.localPosition.x, elevatorEffect.CurrentHeight, elevatorEffectTransformCol.localPosition.z);
            }
        }
    }
}