using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "FollowOwnerState", menuName = "FSM/States/FollowOwnerState")]
public class FollowOwner : State
{
    public override void EnterState(StateMachine stateMachine)
    {
        if (stateMachine.blackboard.Get<NavMeshAgent>(Pet_BBKeys.NavMeshAgent).isStopped)
            stateMachine.blackboard.Get<NavMeshAgent>(Pet_BBKeys.NavMeshAgent).isStopped = false;
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        stateMachine.blackboard.Get<NavMeshAgent>(Pet_BBKeys.NavMeshAgent).SetDestination(stateMachine.blackboard.Get<GameObject>(Pet_BBKeys.OwnerGO).transform.position);
    }
}
