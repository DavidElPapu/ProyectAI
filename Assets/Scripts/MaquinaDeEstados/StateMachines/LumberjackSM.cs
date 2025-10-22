using UnityEngine;
using UnityEngine.AI;

public class LumberjackSM : StateMachine
{
    private void Awake()
    {
        blackboard.Set(LnBNPC_BBKeys.NavMeshAgent, gameObject.GetComponent<NavMeshAgent>());
        blackboard.Set(LnBNPC_BBKeys.GlobalTimeScript, FindAnyObjectByType<GlobalTime>());
        blackboard.Set(LnBNPC_BBKeys.StockpileScript, FindAnyObjectByType<StockpileScript>());
        blackboard.Set(LnBNPC_BBKeys.PickDropWoodSpeed, 0.3f);
        blackboard.Set(LnBNPC_BBKeys.AxeSwingSpeed, 0.9f);
        blackboard.Set(LnBNPC_BBKeys.AxeForce, 1);
        blackboard.Set(LnBNPC_BBKeys.AxeRange, 1.6f);
        blackboard.Set(LnBNPC_BBKeys.WoodInInventory, 0);
        blackboard.Set(LnBNPC_BBKeys.InventoryCapacity, 6);
    }
}

public static class LnBNPC_BBKeys
{
    public const int HomeScript = 0;
    public const int PickDropWoodSpeed = 1;
    public const int StockpileScript = 2;
    public const int AxeSwingSpeed = 3;
    public const int WoodInInventory = 4;
    public const int InventoryCapacity = 5;
    public const int AxeForce = 6;
    public const int AxeRange = 7;
    public const int NavMeshAgent = 8;
    public const int GlobalTimeScript = 9;
    public const int HammerHitSpeed = 10;
    public const int HammerRange = 11;
    public const int HammerForce = 12;
}
