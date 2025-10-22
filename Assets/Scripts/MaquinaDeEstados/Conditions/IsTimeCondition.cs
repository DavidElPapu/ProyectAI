using UnityEngine;

[CreateAssetMenu(fileName = "NewIsTimeCondition", menuName = "FSM/Conditions/IsTimeCondition")]
public class IsTimeCondition : Condition
{
    [SerializeField]
    private int hour, minutes;

    public override bool Check(StateMachine stateMachine)
    {
        int currentHour = stateMachine.blackboard.Get<GlobalTime>(LnBNPC_BBKeys.GlobalTimeScript).GetHour();
        int currentMinutes = stateMachine.blackboard.Get<GlobalTime>(LnBNPC_BBKeys.GlobalTimeScript).GetMinutes();
        if (currentHour == hour && currentMinutes >= minutes)
            return true;
        return false;
    }
}
