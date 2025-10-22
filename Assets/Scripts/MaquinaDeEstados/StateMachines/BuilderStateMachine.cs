using UnityEngine;
using UnityEngine.AI;

public class BuilderStateMachine : StateMachine
{
    private void Awake()
    {
        blackboard.Set(LnBNPC_BBKeys.NavMeshAgent, gameObject.GetComponent<NavMeshAgent>());
        blackboard.Set(LnBNPC_BBKeys.GlobalTimeScript, FindAnyObjectByType<GlobalTime>());
        blackboard.Set(LnBNPC_BBKeys.StockpileScript, FindAnyObjectByType<StockpileScript>());
        blackboard.Set(LnBNPC_BBKeys.PickDropWoodSpeed, 0.3f);
        blackboard.Set(LnBNPC_BBKeys.HammerHitSpeed, 2f);
        blackboard.Set(LnBNPC_BBKeys.HammerForce, 1);
        blackboard.Set(LnBNPC_BBKeys.HammerRange, 3f);
        blackboard.Set(LnBNPC_BBKeys.WoodInInventory, 0);
        blackboard.Set(LnBNPC_BBKeys.InventoryCapacity, 6);
    }
}
