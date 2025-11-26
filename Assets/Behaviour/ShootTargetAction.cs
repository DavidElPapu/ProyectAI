using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ShootTarget", story: "[Agent] shoot [Proyectile] at [Target]", category: "Action", id: "e0c6cdddc541e762ec333005f8aa2085")]
public partial class ShootTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Proyectile;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    public BlackboardVariable<float> bulletSpeed = new BlackboardVariable<float>(10f);

    protected override Status OnStart()
    {
        GameObject proyectileInstance = GameObject.Instantiate(Proyectile.Value, Agent.Value.transform.transform.position, Agent.Value.transform.rotation);
        if (!proyectileInstance.TryGetComponent(out Rigidbody rb))
        {
            rb = proyectileInstance.AddComponent<Rigidbody>();
        }
        Vector3 shootDir = Target.Value.transform.position - Agent.Value.transform.position;
        rb.AddForce(shootDir.normalized * bulletSpeed, ForceMode.Impulse);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

