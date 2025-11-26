using UnityEngine;
using BJs;

[CreateAssetMenu(fileName = "OwnerBoolCondition", menuName = "FSM/Conditions/OwnerBoolCondition")]
public class OwnerBoolCondition : Condition
{
    [SerializeField]
    private bool wantsTrue;

    public override bool Check(StateMachine stateMachine)
    {
        if (stateMachine.blackboard.Get<bool>(Pet_BBKeys.IsOwnerFar) == wantsTrue)
            return true;
        return false;
    }
}
