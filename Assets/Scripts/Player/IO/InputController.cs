using System;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using EditorScripts;
using Interaction;
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
        [SerializeField] private SceneReference mainMenu;
        [SerializeField] private Message message;
        private InputAction _saveAction;
        private InputAction _goBackAction;
        private InOut _io;
        private void Awake()
        {
            var pointController = FindAnyObjectByType<PointController>();
            _io = new InOut(pointController, pointController.GetUpgradeContainer(), message);
            var map = inputAction.FindActionMap("Player");
            _saveAction = map.FindAction("Save");
            _goBackAction = map.FindAction("Escape");
        }
        private void OnEnable()
        {
            inputAction.Enable();
            _saveAction.performed += OnSave;
            _goBackAction.performed += OnBack;
        }
        private void OnDisable()
        {
            inputAction.Disable();
            _saveAction.performed -= OnSave;
            _goBackAction.performed -= OnBack;
        }

        private void OnSave(InputAction.CallbackContext ctx) =>
            _io.Save();
        private void OnBack(InputAction.CallbackContext ctx) => SceneManager.LoadScene(mainMenu.name);
    }
}