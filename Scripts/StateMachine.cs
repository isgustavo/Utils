using System.Collections.Generic;
using UnityEngine;

namespace odt.util
{
    public class StateMachine
    {
        protected Dictionary<string, State> states;

        public State PreviousState { get; private set; }
        public State CurrentState { get; private set; }

        public StateMachine(Dictionary<string, State> states)
        {
            this.states = states;
        }

        public void OnUpdate()
        {
            CurrentState?.OnUpdateState();
        }

        public void OnFixedUpdate()
        {
            CurrentState?.OnFixedUpdateState();
        }

        public void ChangeState(string state)
        {
            if (CurrentState?.GetType().Name == state)
                return;

            if (states.ContainsKey(state))
            {
                CurrentState?.OnLeaveState();
                PreviousState = CurrentState;
                State nextState = states[state];
                nextState.OnEnterState(PreviousState);
                CurrentState = nextState;
            }
            else
            {
                Debug.LogError($"State not found {state}");
            }
        }
    }

}
