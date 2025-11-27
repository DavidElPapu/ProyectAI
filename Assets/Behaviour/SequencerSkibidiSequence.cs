using System;
using Unity.Behavior;
using UnityEngine;
using Composite = Unity.Behavior.Composite;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Sequencer Skibidi", story: "Execute and return Mayority", category: "Flow", id: "78c6b7f3d3b96d7bb8a662d92d4497c4")]
public partial class SequencerSkibidiSequence : Composite
{
    int currentChild = 0;
    float successCount = 0;
    protected override Status OnStart()
    {
        successCount = 0;
        currentChild = 0;
        StartChild(currentChild);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }

    protected Status StartChild(int childIndex)
    {
        if (childIndex >= Children.Count)
        {
            return successCount >= (float)Children.Count / 2 ? Status.Success : Status.Failure;
        }
        var status = StartNode(Children[childIndex]);
        if (status == Status.Success)
        {
            successCount++;
            if (childIndex + 1 >= Children.Count)
            {
                return successCount >= (float)Children.Count / 2 ? Status.Success : Status.Failure;
            }
            return StartChild(childIndex + 1);
        }
        else if (status == Status.Running)
        {
            return Status.Running;
        }
        else
        {
            if (childIndex + 1 >= Children.Count)
            {
                return successCount >= (float)Children.Count / 2 ? Status.Success : Status.Failure;
            }
            return StartChild(childIndex + 1);
        }
    }
}

