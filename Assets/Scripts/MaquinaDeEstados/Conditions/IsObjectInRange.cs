using UnityEngine;

[CreateAssetMenu(fileName = "IsObjectInRangeCondition", menuName = "FSM/Conditions/IsObjectInRangeCondition")]
public class IsObjectInRange : Condition
{
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private bool needsPlayerInRange;

    public override bool Check(StateMachine stateMachine)
    {
        Collider[] objsInRange = Physics.OverlapSphere(stateMachine.transform.position, stateMachine.blackboard.Get<float>(Pet_BBKeys.OwnerDetectionRange), playerLayer);
        foreach (Collider obj in objsInRange)
        {
            if (obj.gameObject == stateMachine.blackboard.Get<GameObject>(Pet_BBKeys.OwnerGO))
            {
                if (needsPlayerInRange)
                    return true;
                else
                    return false;
            }
        }
        if (needsPlayerInRange)
            return false;
        else
            return true;
    }
}
