using System;
using Voody.UniLeo.Lite;

[Serializable]
struct DoorComponent
{
    public bool IsOpen; 
}

class DoorProvider : MonoProvider<DoorComponent>
{
}