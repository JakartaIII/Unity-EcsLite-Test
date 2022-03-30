using Leopotam.EcsLite;
using UnityEngine;

class PlayerPointAndClickSystem : IEcsRunSystem
{
    public void Run(EcsSystems systems)
    {
        var filter = systems.GetWorld().Filter<PlayerComponent>().End();
        var poolPlayer = systems.GetWorld().GetPool<PlayerComponent>();
        foreach (int i in filter)
        {
            ref var player = ref poolPlayer.Get(i);

            if (Input.GetMouseButtonDown(0))
            { 
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    player.Agent.SetDestination(hit.point);
                    player.WayTargetSphere.position = hit.point;
                    // Do something with the object that was hit by the raycast.
                }
            }

            float normilizedVelocity = player.Agent.velocity.magnitude/ player.Agent.speed; 
            player.Anim.SetFloat("Walk", normilizedVelocity);
        }
    }
}