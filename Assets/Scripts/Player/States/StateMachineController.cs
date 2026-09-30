using System;
using UnityEngine;

namespace Player.States
{
    public class StateMachineController : MonoBehaviour
    {
        [SerializeField] private GameObject shopMenu;
        [SerializeField] private GameObject popMenu;
        private StateMachine _stateMachine;
        private OnGameState _onGameState;
        private OnMenuState _savePopMenuState;
        private OnMenuState _shopMenuState;
        
        public void Awake()
        {
            _onGameState = new OnGameState();
            _savePopMenuState = new OnMenuState(popMenu);
            _shopMenuState = new OnMenuState(shopMenu);
            _stateMachine = new StateMachine(_onGameState);
        }
    }
}