using UnityEngine;

[CreateAssetMenu(fileName = "RaycastHitCondition", menuName = "FSM/Conditions/RaycastHitCondition")]
public class RaycastHitCondition : Condition
{
    public float rayDistance;
    public Vector3 rayDirection;
    public string hitObjectTag;
    public LayerMask hitObjectMask;

    public override bool Check(StateMachine stateMachine)
    {
        Ray ray = new Ray(stateMachine.transform.position, rayDirection);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, hitObjectMask))
        {
            if (hit.collider.gameObject.CompareTag(hitObjectTag))
                return true;
        }
        return false;
    }
}
