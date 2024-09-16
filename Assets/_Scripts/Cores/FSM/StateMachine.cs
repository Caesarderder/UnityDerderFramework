using System;
using UnityEngine;

namespace FSM
{
    [Serializable]
    public class StateMachine
    {
        public StateMachine()
        {
        }
        public string StateName;
        public State CurrentState;

        public void Init(State state)
        {
            CurrentState = state;
            CurrentState.Enter();
            StateName=CurrentState.ToString();
        }
        
        public void ChangeState(State newState)
        {
           CurrentState.Exit();
           CurrentState = newState;
            StateName = CurrentState.ToString();
            CurrentState.Enter();
        }
    }
}