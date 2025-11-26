using UnityEngine;
using BJs;

[CreateAssetMenu(fileName = "NewInventoryCondition", menuName = "FSM/Conditions/InventoryCondition")]
public class InventoryCondition : Condition
{
    [SerializeField]
    private bool needsEmptyInventory;

    public override bool Check(StateMachine stateMachine)
    {
        if (needsEmptyInventory && stateMachine.blackboard.Get<int>(LnBNPC_BBKeys.WoodInInventory) == 0)
            return true;
        else if (!needsEmptyInventory && stateMachine.blackboard.Get<int>(LnBNPC_BBKeys.WoodInInventory) == stateMachine.blackboard.Get<int>(LnBNPC_BBKeys.InventoryCapacity))
            return true;
        return false;
    }
}
