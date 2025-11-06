using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[CreateAssetMenu(fileName = "WaitForOwnerState", menuName = "FSM/States/WaitForOwnerState")]

public class WaitForOwnerState : State
{
    [SerializeField]
    private float waitSeconds;

    public override void EnterState(StateMachine stateMachine)
    {
        if (stateMachine.blackboard.Get<NavMeshAgent>(Pet_BBKeys.NavMeshAgent).hasPath)
            stateMachine.blackboard.Get<NavMeshAgent>(Pet_BBKeys.NavMeshAgent).isStopped = true;
        stateMachine.blackboard.ChangeValue(Pet_BBKeys.IsOwnerFar, false);
        stateMachine.StartCoroutine(GoHome(stateMachine));
    }

    private IEnumerator GoHome(StateMachine stateMachine)
    {
        yield return new WaitForSeconds(waitSeconds);
        stateMachine.blackboard.ChangeValue(Pet_BBKeys.IsOwnerFar, true);
        yield break;
    }
}
