using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "EnemyWanderState", menuName = "FSM/States/EnemyWanderState")]
public class EnemyWanderState : State
{
    private Vector3 direction = Vector3.zero;
    private Coroutine coroutine;

    public override void EnterState(StateMachine stateMachine)
    {
        if (coroutine == null)
        {
            coroutine = stateMachine.StartCoroutine(SwitchDirection(stateMachine));
        }
    }

    public override void ExitState(StateMachine stateMachine)
    {
        if(coroutine != null)
        {
            stateMachine.StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private IEnumerator SwitchDirection(StateMachine stateMachine)
    {
        while (true)
        {
            float randX = Random.Range(-1f, 1f);
            float randZ = Random.Range(-1f, 1f);
            direction = new Vector3(randX, 0, randZ);
            stateMachine.blackboard.Get<NavMeshAgent>(Enemy_BBKeys.NavMeshAgent).Move(direction);
            yield return new WaitForSeconds(stateMachine.blackboard.Get<float>(Enemy_BBKeys.WonderDirectionChangeInterval));
        }
    }
}
