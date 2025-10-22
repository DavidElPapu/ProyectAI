using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "NewRestingState", menuName = "FSM/States/RestingState")]
public class RestingState : State
{
    private bool isInHome;

    public override void EnterState(StateMachine stateMachine)
    {
        if (CheckIfIsHome(stateMachine.transform, stateMachine))
            isInHome = true;
        else
        {
            isInHome = false;
            Vector3 homePos = stateMachine.blackboard.Get<NPCHomeScript>(LnBNPC_BBKeys.HomeScript).homeEntrance.position;
            if (stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped)
                stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped = false;
            stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).SetDestination(homePos);
        }
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        if (!isInHome)
        {
            if (CheckIfIsHome(stateMachine.transform, stateMachine))
                isInHome = true;
        }
    }

    public override void ExitState(StateMachine stateMachine)
    {
        stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped = true;
    }

    private bool CheckIfIsHome(Transform npcTransform, StateMachine stateMachine)
    {
        Vector3 homeEntrancePos = stateMachine.blackboard.Get<NPCHomeScript>(LnBNPC_BBKeys.HomeScript).homeEntrance.position;
        Vector3 homeInsidePos = stateMachine.blackboard.Get<NPCHomeScript>(LnBNPC_BBKeys.HomeScript).homeInside.position;
        if (Vector3.Distance(npcTransform.position, homeEntrancePos) <= 0.1f)
        {
            Debug.Log("Llegue a la entrada");
            stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped = true;
            npcTransform.position = stateMachine.blackboard.Get<NPCHomeScript>(LnBNPC_BBKeys.HomeScript).homeInside.position;
            return true;
        }
        else if(Vector3.Distance(npcTransform.position, homeInsidePos) <= 0.1f)
        {
            Debug.Log("Estoy dentro");
            stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped = true;
            return true;
        }
        return false;
    }
}
