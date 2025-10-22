using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "NewGoToStockpileState", menuName = "FSM/States/GoToStockpileState")]
public class GoToStockpileState : State
{
    [SerializeField]
    private bool takeFromStockpile;
    private bool isCloseToStockpile;
    private float dropCount;

    public override void EnterState(StateMachine stateMachine)
    {
        if (CheckIfIsCloseToStockpile(stateMachine.transform, stateMachine))
            isCloseToStockpile = true;
        else
        {
            isCloseToStockpile = false;
            Vector3 stockpilePos = stateMachine.blackboard.Get<StockpileScript>(LnBNPC_BBKeys.StockpileScript).gameObject.transform.position;
            if (stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped)
                stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped = false;
            stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).SetDestination(stockpilePos);
        }
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        if (!isCloseToStockpile)
        {
            if (CheckIfIsCloseToStockpile(stateMachine.transform, stateMachine))
            {
                dropCount = stateMachine.blackboard.Get<float>(LnBNPC_BBKeys.PickDropWoodSpeed);
                isCloseToStockpile = true;
            }
        }
        else
        {
            if (dropCount > 0)
            {
                dropCount -= Time.deltaTime;
                if (dropCount <= 0)
                {
                    if (takeFromStockpile)
                    {
                        if (stateMachine.blackboard.Get<StockpileScript>(LnBNPC_BBKeys.StockpileScript).CanPickWood())
                        {
                            int newInventory = stateMachine.blackboard.Get<int>(LnBNPC_BBKeys.WoodInInventory) + 1;
                            if (newInventory > stateMachine.blackboard.Get<int>(LnBNPC_BBKeys.InventoryCapacity))
                                newInventory = stateMachine.blackboard.Get<int>(LnBNPC_BBKeys.InventoryCapacity);
                            stateMachine.blackboard.ChangeValue(LnBNPC_BBKeys.WoodInInventory, newInventory);
                        }
                    }
                    else
                    {
                        if(stateMachine.blackboard.Get<int>(LnBNPC_BBKeys.WoodInInventory) > 0)
                        {
                            stateMachine.blackboard.Get<StockpileScript>(LnBNPC_BBKeys.StockpileScript).DepositWood();
                            int newInventory = stateMachine.blackboard.Get<int>(LnBNPC_BBKeys.WoodInInventory) - 1;
                            if (newInventory < 0)
                                newInventory = 0;
                            stateMachine.blackboard.ChangeValue(LnBNPC_BBKeys.WoodInInventory, newInventory);
                        }
                    }
                    dropCount = stateMachine.blackboard.Get<float>(LnBNPC_BBKeys.PickDropWoodSpeed);
                }
            }
        }
    }

    public override void ExitState(StateMachine stateMachine)
    {
        stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped = true;
    }

    private bool CheckIfIsCloseToStockpile(Transform npcTransform, StateMachine stateMachine)
    {
        Vector3 stockpilePos = stateMachine.blackboard.Get<StockpileScript>(LnBNPC_BBKeys.StockpileScript).gameObject.transform.position;
        Vector3 stockpileAdjustedPos = new Vector3(stockpilePos.x, stateMachine.transform.position.y, stockpilePos.z);
        if (Vector3.Distance(npcTransform.position, stockpileAdjustedPos) <= 3f)
        {
            stateMachine.blackboard.Get<NavMeshAgent>(LnBNPC_BBKeys.NavMeshAgent).isStopped = true;
            return true;
        }
        return false;
    }
}
