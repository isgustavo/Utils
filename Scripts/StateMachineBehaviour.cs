using System.Collections.Generic;
using UnityEngine;
using odt.util;

public abstract class StateMachineBehaviour : MonoBehaviour
{
    protected Dictionary<string, State> states = new Dictionary<string, State>();

    public State PreviousState { get; private set; } 
    public State CurrentState { get; protected set; }

    protected virtual void Awake()
    {
        Setup();
    }

    protected virtual void Start()
    {
        if(CurrentState == null)
        {
            Debug.LogError($"Current State not found {CurrentState}");
        }
    }

    protected virtual void Update()
    {
        CurrentState.OnUpdateState();
    }

    protected virtual void FixedUpdate()
    {
        CurrentState.OnFixedUpdateState();
    }

    protected abstract void Setup();

    public void ChangeState(string state)
    {
        if (CurrentState?.GetType().ToString() == state)
            return;

        if (states.ContainsKey(state))
        {
            CurrentState?.OnLeaveState();
            State nextState = states[state];
            PreviousState = CurrentState;
            nextState.OnEnterState(PreviousState);
            CurrentState = nextState;
        }
        else
        {
            Debug.LogError($"State not found {state}");
        } 
    }
}
