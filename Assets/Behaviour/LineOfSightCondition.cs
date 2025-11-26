using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "LineOfSightCondition", story: "[Agent] has line of sight of [Object]", category: "Conditions", id: "c61bf492c20811df5c00cac7c51ebd63")]
public partial class LineOfSightCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Object;

    public override bool IsTrue()
    {
        Ray sight = new Ray(Agent.Value.transform.position, Object.Value.transform.position - Agent.Value.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(sight, out hit))
        {
            if (hit.collider.gameObject == Object.Value)
                return true;
        }
        return false;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
