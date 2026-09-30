using EditorScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player.States
{
    public class StateMachineController : MonoBehaviour
    {
        [SerializeField] private GameObject shopMenu;
        [SerializeField] private GameObject popMenu;
        [SerializeField] private SceneReference mainMenu;
        private StateMachine _stateMachine;
        private OnGameState _onGameState;
        private OnMenuState _exitPopState;
        private OnMenuState _shopMenuState;
        
        public void Awake()
        {
            _onGameState = new OnGameState();
            _exitPopState = new OnMenuState(popMenu);
            _shopMenuState = new OnMenuState(shopMenu);
            _stateMachine = new StateMachine(_onGameState);
        }
        private bool InGame => _stateMachine.CurrentState() is OnGameState;
        private bool InShop => _stateMachine.CurrentState() is OnMenuState;
        public bool CanSave => InGame;

        public void ToggleShop()
        {
            if(InGame) _stateMachine.ChangeState(_shopMenuState);
            else if(InShop) _stateMachine.ChangeState(_onGameState);
        }
        public void Escape()
        {
            if (InGame) _stateMachine.ChangeState(_exitPopState);
            else _stateMachine.ChangeState(_onGameState);
        }
        public void ConfirmExit() => SceneManager.LoadScene(mainMenu.name);
        public void ConfirmDeny() => popMenu.SetActive(false);
    }
}