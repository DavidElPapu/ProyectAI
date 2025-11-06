using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "GoHomeState", menuName = "FSM/States/GoHomeState")]

public class GoHomeState : State
{
    public override void EnterState(StateMachine stateMachine)
    {
        if (stateMachine.blackboard.Get<NavMeshAgent>(Pet_BBKeys.NavMeshAgent).isStopped)
            stateMachine.blackboard.Get<NavMeshAgent>(Pet_BBKeys.NavMeshAgent).isStopped = false;
        stateMachine.blackboard.Get<NavMeshAgent>(Pet_BBKeys.NavMeshAgent).SetDestination(stateMachine.blackboard.Get<Transform>(Pet_BBKeys.HomeTransform).position);
    }
}
