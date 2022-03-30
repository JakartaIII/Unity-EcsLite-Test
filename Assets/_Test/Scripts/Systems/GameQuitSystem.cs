using Leopotam.EcsLite;
using UnityEngine;

internal class GameQuitSystem : IEcsRunSystem
{
    public void Run(EcsSystems systems)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            Application.Quit();
        }
    }
}