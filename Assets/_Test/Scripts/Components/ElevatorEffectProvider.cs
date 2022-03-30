using System;
using UnityEngine;
using Voody.UniLeo.Lite;

[Serializable]
struct ElevatorEffectComponent
{
    public float CurrentHeight;
    public float MaxHeight;
    public float MinHeight;
    public bool IsUp;
    public bool Update;
    public float SecondsToLift;
    public Transform TransformCol;
}

class ElevatorEffectProvider : MonoProvider<ElevatorEffectComponent>
{

}