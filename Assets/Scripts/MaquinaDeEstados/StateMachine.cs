using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    public State initialState;
    public State currentState;
    public Blackboard blackboard = new Blackboard();

    protected virtual void Start()
    {
        ChangeState(initialState);
    }

    protected virtual void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
            currentState.CheckTransitions(this);
        }
    }

    public virtual void ChangeState(State state)
    {
        if (currentState == state || state == null) return;
        if (currentState != null)
            currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
}
