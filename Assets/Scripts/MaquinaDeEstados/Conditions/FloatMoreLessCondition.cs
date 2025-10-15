using UnityEngine;

[CreateAssetMenu(fileName = "NewFloatMoreLessCondition", menuName = "FSM/Conditions/FloatMoreLessCondition")]
public class FloatMoreLessCondition : Condition
{
    public string floatName;
    public float floatValue;
    public bool isLessEqualThan;

    public override bool Check(StateMachine stateMachine)
    {
        foreach (StateMachineData data in stateMachine.context)
        {
            if (data.dataName == floatName)
            {
                if (isLessEqualThan && data.floatData <= floatValue)
                    return true;
                else if (!isLessEqualThan && data.floatData >= floatValue)
                    return true;
            }
        }
        return false;
    }
}
