using UnityEngine;
namespace Player.States
{
    public class OnMenuState : State
    {
        private readonly GameObject _menu;

        public OnMenuState(GameObject menu)
        {
            _menu = menu;
        }
        public void OnStateEnter()
        {
            _menu.SetActive(true);
        }
        public void OnStateExit()
        {
            _menu.SetActive(false);
        }

    }
}