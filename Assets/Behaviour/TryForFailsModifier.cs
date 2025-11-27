using System;
using Unity.Behavior;
using UnityEngine;
using Modifier = Unity.Behavior.Modifier;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Try For Fails", story: "Try [Number] of Failures", category: "Flow", id: "25feed5d8d455f1a825ca201bb16950a")]
public partial class TryForFailsModifier : Modifier
{
    [SerializeReference] public BlackboardVariable<int> Number;
    internal int attemptCount = 0;

    protected override Status OnStart()
    {
        attemptCount = attemptCount > 0 ? attemptCount : 1;
        if (Child == null)
        {
            return Status.Failure;
        }
        var status = StartNode(Child);
        if (status == Status.Failure || status == Status.Success)
        {
            return Status.Running;
        }
        return Status.Waiting;
    }

    protected override Status OnUpdate()
    {
        if (attemptCount >= Number.Value)
        {
            attemptCount = 0;
            Debug.Log("Me gusta comer fetos");
            return Status.Failure;
        }

        Status status = Child.CurrentStatus;
        if (status == Status.Failure)
        {
            attemptCount++;
            Debug.Log($"Failed attempt {attemptCount}!");
            return Status.Running;
        }
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

