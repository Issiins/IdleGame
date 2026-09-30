using System;
using UnityEngine;

namespace Player.States
{
    public class StateMachine
    {
        private State _currentState;
        private State _prevState;
        
        public void ChangeState(State newState)
        {
            _currentState.OnStateExit();
            _currentState = newState;
            _currentState.OnStateEnter();
        }
    }
}