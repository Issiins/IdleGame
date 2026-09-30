using System;
using UnityEngine;

namespace Player.States
{
    public class StateMachine
    {
        private State _currentState;
        public StateMachine(State initState)
        {
            EnterState(initState);
        }
        public void ChangeState(State newState)
        {
            _currentState.OnStateExit();
            EnterState(newState);
        }

        private void EnterState(State newState)
        {
            _currentState = newState;
            _currentState.OnStateEnter();
        }
    }
}