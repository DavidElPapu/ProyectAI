using System.Collections.Generic;
using UnityEngine;


namespace BehaviourTrees
{
    public class Sequence : Node
    {
        public Sequence(string  name) : base(name) { }
        public override Status Process()
        {
            if (currentChild < children.Count)
            {
                switch (children[currentChild].Process())
                {
                    case Status.Success:
                        currentChild++;
                        return currentChild == children.Count ? Status.Success : Status.Running;
                    case Status.Failure:
                        Reset();
                        return Status.Failure;
                    case Status.Running:
                        Reset();
                        return Status.Running;
                    default:
                        break;
                }
            }
            Reset();
            return Status.Success;
        }
    }

    public class Selector : Node
    {
        public Selector(string name) : base(name) { }
        public override Status Process()
        {
            foreach (var child in children)
            {
                switch (children[currentChild].Process())
                {
                    case Status.Success:
                        return Status.Success;
                    case Status.Running:
                        return Status.Running;
                    case Status.Failure:
                        continue;
                    default:
                        break;
                }
            }
            return Status.Failure;
        }
    }
    #region Base
    public class Node
    {
        public enum Status
        {
            Success,
            Failure,
            Running
        }

        public readonly string name;

        public readonly List<Node> children = new List<Node>();
        protected int currentChild = 0;

        public Node(string name)
        {
            this.name = name;
        }

        public void AddChild(Node child)
        {
            children.Add(child);
        }

        public virtual Status Process() => children[currentChild].Process();

        public virtual void Reset()
        {
            currentChild = 0;
            foreach (Node child in children)
            {
                child.Reset();
            }
        }
    }

    public class Leaf : Node
    {
        readonly IStrategy strategy;

        public Leaf(string name, IStrategy strategy) : base(name)
        {
            this.strategy = strategy;
        }

        public override Status Process() => strategy.Process();

        public override void Reset() => strategy.Reset();
    }

    public class BehaviourTree : Node
    {
        public BehaviourTree(string name) : base(name) { }

        public override Status Process()
        {
            while (currentChild < children.Count)
            {
                var status = children[currentChild].Process();

                if (status != Status.Success)
                {
                    return status;
                }
                currentChild++;
            }
            return Status.Success;
        }
    }
    #endregion
}
