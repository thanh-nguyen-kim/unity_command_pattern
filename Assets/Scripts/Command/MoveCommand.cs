using UnityEngine;
using UnityEngine.AI;
public class MoveCommand : Command
{
    public Vector3 target;
    private GameObject actor;
    public MoveCommand(Vector3 target)
    {
        name = "Move";
        this.target = target;
    }
    public override void Execute(GameObject actor)
    {
        this.actor = actor;
        var navAgent = actor.GetComponent<NavMeshAgent>();
        actor.GetComponent<CharacterController>().PlayAnimation(CharacterController.WALK);
        NavMeshHit targetProjection;
        if (NavMesh.SamplePosition(target, out targetProjection, 4f, NavMesh.AllAreas))
        {
            target = targetProjection.position;
            navAgent.SetDestination(target);
        }
    }
    public override bool IsCompleted()
    {
        return Vector3.Distance(actor.transform.position, target) < 1.25f && actor.GetComponent<NavMeshAgent>().velocity.sqrMagnitude < 0.1f;
    }
}