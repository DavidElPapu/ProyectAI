using UnityEngine;

[CreateAssetMenu(fileName = "NewIsTimeCondition", menuName = "FSM/Conditions/IsTimeCondition")]
public class IsTimeCondition : Condition
{
    public string timeManagerObjectName;
    public int Hour, minute;

    public override bool Check(StateMachine stateMachine)
    {
        foreach (StateMachineData data in stateMachine.context)
        {
            if (data.dataName == timeManagerObjectName)
            {
                if (data.objectData.TryGetComponent(out GlobalTime timeManager))
                {
                    if (timeManager.GetHour() == Hour && timeManager.GetMinutes() <= minute)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
