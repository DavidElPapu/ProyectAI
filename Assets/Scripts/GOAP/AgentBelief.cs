using System;
using System.Collections.Generic;
using UnityEngine;

public class AgentBelief : MonoBehaviour
{
    #region BaseBelief
    public string Name;

    Func<bool> condition = () => false;

    Func<Vector3> observedLocation = () => Vector3.zero;

    public Vector3 Location => observedLocation();

    AgentBelief(string name)
    {
        Name = name;
    }

    public bool Evaluate() => condition();
    #endregion
    #region Builder
    public class Builder
    {
        public readonly AgentBelief belief;

        public Builder(string name)
        {
            belief = new AgentBelief(name);
        }

        public Builder WithCondition(Func<bool> condition)
        {
            belief.condition = condition;
            return this;
        }

        public Builder WithLocation(Func<Vector3> location)
        {
            belief.observedLocation = location;
            return this;
        }

        public AgentBelief Build()
        {
            return belief;
        }
    }
    #endregion
    #region Factory
    public class BeliefFactory
    {
        readonly GoapAgent agent;
        readonly Dictionary<string, AgentBelief> beliefs;

        public BeliefFactory(GoapAgent agent, Dictionary<string, AgentBelief> beliefs)
        {
            this.agent = agent;
            this.beliefs = beliefs;
        }

        public void AddBelief(string key, Func<bool> condition)
        {
            beliefs.Add(key, new AgentBelief.Builder(key)
                .WithCondition(condition)
                .Build());
        }

        public void AddLocationBelief(string key, float distance, Transform locationCondition)
        {
            AddLocationBelief(key, distance, locationCondition.position);
        }

        public void AddLocationBelief(string key, float distance, Vector3 locationCondition)
        {
            beliefs.Add(key, new AgentBelief.Builder(key)
                .WithCondition(() => InRangeOf(locationCondition, distance))
                .WithLocation(() => locationCondition)
                .Build());
        }

        private bool InRangeOf(Vector3 position, float distance)
        {
            return Vector3.Distance(agent.transform.position, position) < distance;
        }
    }
    #endregion
}
