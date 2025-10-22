using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "NewBuildingState", menuName = "FSM/States/BuildingState")]
public class BuildingState : State
{
    public LayerMask houseLayer;
    private float houseDetectionRange = 1000f;
    private float rotationSpeed = 2f;
    private GameObject houseTarget;
    private bool isBuilding;
    private float hammerCount;

    public override void EnterState(StateMachine stateMachine)
    {
        if (CheckIfIsInsideHome(stateMachine.transform, stateMachine))
            stateMachine.transform.position = stateMachine.blackboard.Get<NPCHomeScript>(LnBNPC_BBKeys.HomeScript).homeEntrance.position;
        houseTarget = null;
        isBuilding = false;
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        if (houseTarget == null)
        {
            stateMachine.transform.Rotate(new Vector3(0, 1, 0) * rotationSpeed);
            LookForHouse(stateMachine);
        }
        else
        {
            if (!isBuilding)
                IsCloseToHouse(stateMachine);
            else
            {
                if (hammerCount > 0)
                {
                    hammerCount -= Time.deltaTime;
                    if (hammerCount <= 0)
                    {
                        if (houseTarget.TryGetComponent(out NPCHomeScript houseScript))
                        {
                            if (houseScript.CanBuildHere())
                            {
                                int woodUsed = 0;
                                if (stateMachine.blackboard.Get<int>(LnBNPC_BBKeys.WoodInInventory) >= stateMachine.blackboard.Get<int>(LnBNPC_BBKeys.HammerForce))
                                    woodUsed = stateMachine.blackboard.Get<int>(LnBNPC_BBKeys.HammerForce);
                                else
                                    woodUsed = stateMachine.blackboard.Get<int>(LnBNPC_BBKeys.WoodInInventory);
                                houseScript.AddWood(woodUsed);
                                int newInventory = stateMachine.blackboard.Get<int>(LnBNPC_BBKeys.WoodInInventory) - woodUsed;
                                stateMachine.blackboard.ChangeValue(LnBNPC_BBKeys.WoodInInventory, newInventory);
                                hammerCount = stateMachine.blackboard.Get<float>(LnBNPC_BBKeys.HammerHitSpeed);
                            }
                            else
                            {
                                houseTarget = null;
                                isBuilding = false;
                            }
                        }
                    }
                }
            }
        }
    }

    public override void ExitState(StateMachine stateMachine)
    {
        stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped = true;
    }

    private bool IsCloseToHouse(StateMachine stateMachine)
    {
        Vector3 houseFixedPos = new Vector3(houseTarget.transform.position.x, stateMachine.transform.position.y, houseTarget.transform.position.z);
        if (Vector3.Distance(stateMachine.transform.position, houseFixedPos) <= stateMachine.blackboard.Get<float>(LnBNPC_BBKeys.HammerRange))
        {
            stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped = true;
            isBuilding = true;
            hammerCount = stateMachine.blackboard.Get<float>(LnBNPC_BBKeys.HammerHitSpeed);
            return true;
        }
        return false;
    }

    private void LookForHouse(StateMachine stateMachine)
    {
        RaycastHit[] lookedObjects = Physics.RaycastAll(stateMachine.transform.position, stateMachine.transform.forward, houseDetectionRange, houseLayer, QueryTriggerInteraction.Collide);
        if (lookedObjects.Length > 0)
        {
            foreach (RaycastHit hitObj in lookedObjects)
            {
                if (hitObj.collider.gameObject.CompareTag("House"))
                {
                    if (hitObj.collider.gameObject.TryGetComponent(out NPCHomeScript houseScript))
                    {
                        if (houseScript.CanBuildHere())
                        {
                            houseTarget = hitObj.collider.gameObject;
                            if (stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped)
                                stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped = false;
                            stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).SetDestination(houseTarget.transform.position);
                            return;
                        }
                    }
                }
            }
        }
    }

    private bool CheckIfIsInsideHome(Transform npcTransform, StateMachine stateMachine)
    {
        Vector3 homeInsidePos = stateMachine.blackboard.Get<NPCHomeScript>(LnBNPC_BBKeys.HomeScript).homeInside.position;
        if (Vector3.Distance(npcTransform.position, homeInsidePos) <= 0.1f)
        {
            stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped = true;
            return true;
        }
        return false;
    }
}
