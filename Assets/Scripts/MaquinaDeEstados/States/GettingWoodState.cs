using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "NewGettingWoodState", menuName = "FSM/States/GettingWoodState")]
public class GettingWoodState : State
{
    public LayerMask treeLayer;
    private float treeDetectionRange = 1000f;
    private float rotationSpeed = 2f;
    private GameObject treeTarget;
    private bool isChopping;
    private float chopCount;

    public override void EnterState(StateMachine stateMachine)
    {
        if (CheckIfIsInsideHome(stateMachine.transform, stateMachine))
            stateMachine.transform.position = stateMachine.blackboard.Get<NPCHomeScript>(LnBNPC_BBKeys.HomeScript).homeEntrance.position;
        treeTarget = null;
        isChopping = false;
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        if (treeTarget == null)
        {
            stateMachine.transform.Rotate(new Vector3(0, 1, 0) * rotationSpeed);
            LookForTree(stateMachine);
        }
        else
        {
            if (!isChopping)
                IsCloseToTree(stateMachine);
            else
            {
                if (chopCount > 0)
                {
                    chopCount -= Time.deltaTime;
                    if (chopCount <= 0)
                    {
                        if (treeTarget.GetComponent<TreeScript>().IsTreeChopped(stateMachine.blackboard.Get<int>(LnBNPC_BBKeys.AxeForce)))
                        {
                            int newInventory = stateMachine.blackboard.Get<int>(LnBNPC_BBKeys.WoodInInventory) + treeTarget.GetComponent<TreeScript>().GetWoodAmount();
                            if (newInventory > stateMachine.blackboard.Get<int>(LnBNPC_BBKeys.InventoryCapacity))
                                newInventory = stateMachine.blackboard.Get<int>(LnBNPC_BBKeys.InventoryCapacity);
                            stateMachine.blackboard.ChangeValue(LnBNPC_BBKeys.WoodInInventory, newInventory);
                            Destroy(treeTarget);
                            treeTarget = null;
                            isChopping = false;
                        }
                        else
                            chopCount = stateMachine.blackboard.Get<float>(LnBNPC_BBKeys.AxeSwingSpeed);
                    }
                }
            }
        }
    }

    public override void ExitState(StateMachine stateMachine)
    {
        stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped = true;
    }

    private bool IsCloseToTree(StateMachine stateMachine)
    {
        Vector3 treeFixedPos = new Vector3(treeTarget.transform.position.x, stateMachine.transform.position.y, treeTarget.transform.position.z);
        if (Vector3.Distance(stateMachine.transform.position, treeFixedPos) <= stateMachine.blackboard.Get<float>(LnBNPC_BBKeys.AxeRange))
        {
            stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped = true;
            isChopping = true;
            chopCount = stateMachine.blackboard.Get<float>(LnBNPC_BBKeys.AxeSwingSpeed);
            return true;
        }
        return false;
    }

    private void LookForTree(StateMachine stateMachine)
    {
        RaycastHit[] lookedObjects = Physics.RaycastAll(stateMachine.transform.position, stateMachine.transform.forward, treeDetectionRange, treeLayer);
        if (lookedObjects.Length > 0)
        {
            foreach (RaycastHit hitObj in lookedObjects)
            {
                if (hitObj.collider.gameObject.CompareTag("Tree"))
                {
                    treeTarget = hitObj.collider.gameObject;
                    if (stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped)
                        stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped = false;
                    stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).SetDestination(treeTarget.transform.position);
                    return;
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
