using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    public State initialState;
    public State currentState;
    public List<StateMachineData> context;

    private void Start()
    {
        ChangeState(initialState);
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
            currentState.CheckTransitions(this);
        }
    }

    public void ChangeState(State state)
    {
        if (currentState == state || state == null) return;
        if (currentState != null)
            currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
}

[Serializable]
public struct StateMachineData
{
    public bool boolData;
    public float floatData;
    public Vector3 vectorData;
    public NavMeshAgent agentData;
    public GameObject objectData;
    public string dataName;

    public StateMachineData(bool boolData, float floatData, Vector3 vectorData, NavMeshAgent agentData, GameObject objectData, string dataName)
    {
        this.boolData = boolData;
        this.floatData = floatData;
        this.vectorData = vectorData;
        this.agentData = agentData;
        this.objectData = objectData;
        this.dataName = dataName;
    }
}
