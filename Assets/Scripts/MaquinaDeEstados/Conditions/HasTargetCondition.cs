using UnityEngine;

[CreateAssetMenu(fileName = "NewHasTargetCondition", menuName = "FSM/Conditions/HasTargetCondition")]
public class HasTargetCondition : Condition
{
    public bool needsTarget;

    public override bool Check(StateMachine stateMachine)
    {
        foreach (StateMachineData data in stateMachine.context)
        {
            if (data.dataName == "TargetPos")
            {
                if (needsTarget && data.objectData != null)
                    return true;
                else if (!needsTarget && data.objectData == null)
                    return true;
            }
        }
        return false;
    }
}
