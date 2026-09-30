using System;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using EditorScripts;
using Interaction;
using Player.States;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Upgrades;

namespace Player
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputAction;
        [SerializeField] private Message message;
        [SerializeField] private StateMachineController  stateMachine;
        private InputAction _saveAction;
        private InputAction _goBackAction;
        private InputAction _onShopAction;
        private InOut _io;
        private void Awake()
        {
            var pointController = FindAnyObjectByType<PointController>();
            _io = new InOut(pointController, pointController.GetUpgradeContainer(), message);
            var map = inputAction.FindActionMap("Player");
            _saveAction = map.FindAction("Save");
            _goBackAction = map.FindAction("Escape");
            _onShopAction = map.FindAction("Shop");
        }
        private void OnEnable()
        {
            inputAction.Enable();
            _saveAction.performed += OnSave;
            _goBackAction.performed += OnBack;
            _onShopAction.performed += OnShopMenu;
        }
        private void OnDisable()
        {
            inputAction.Disable();
            _saveAction.performed -= OnSave;
            _goBackAction.performed -= OnBack;
            _onShopAction.performed -= OnShopMenu;
        }

        private void OnSave(InputAction.CallbackContext ctx)
        {
            if(!stateMachine.CanSave) return; 
            _io.Save();
        }
        private void OnBack(InputAction.CallbackContext ctx) => stateMachine.Escape();
        private void OnShopMenu(InputAction.CallbackContext ctx) => stateMachine.ToggleShop();
    }
}