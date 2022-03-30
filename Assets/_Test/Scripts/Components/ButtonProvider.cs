using System; 
using Voody.UniLeo.Lite;

[Serializable]
struct ButtonComponent
{ 
    public bool IsPressed; 
    public ConvertToEntity DoorConvertToEntity;
}

class ButtonProvider : MonoProvider<ButtonComponent>
{
    
}

