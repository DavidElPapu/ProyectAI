using UnityEngine;

[CreateAssetMenu(fileName = "RotateState", menuName = "FSM/States/RotateState")]
public class RotateState : State
{
    public float rotationSpeed = 2f;

    public override void UpdateState(StateMachine stateMachine)
    {
        stateMachine.transform.Rotate(new Vector3(90, 90, 90) * rotationSpeed * Time.deltaTime);
        if (stateMachine.blackboard.Get<float>(BBKeys.Battery) == default(float))
        {
            stateMachine.blackboard.Set(BBKeys.Battery, 100f);
        }
    }
}
