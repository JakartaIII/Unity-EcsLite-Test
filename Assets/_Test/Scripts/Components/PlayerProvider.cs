using System; 
using UnityEngine;
using UnityEngine.AI;
using Voody.UniLeo.Lite;

[Serializable]
struct PlayerComponent
{
    public Transform Transform;
    public Transform WayTargetSphere;
    public NavMeshAgent Agent;
    public Animator Anim;
    
}

class PlayerProvider : MonoProvider<PlayerComponent>
{
 
}