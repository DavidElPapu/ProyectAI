using UnityEngine;

namespace BJs
{
    public abstract class Condition : ScriptableObject
    {
        public virtual bool Check(StateMachine stateMachine) { return false; }
    }

    [System.Serializable]
    public class Transition
    {
        public Condition condition;
        public State state;
    }
}
