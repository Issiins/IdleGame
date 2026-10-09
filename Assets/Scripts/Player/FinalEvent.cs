using System;
using Interaction;
using TMPro;
using UnityEngine;

namespace Player
{
    public class FinalEvent : MonoBehaviour
    {
        [SerializeField] PointController pointController;

        private void Awake()
        {
            pointController.GetUpgradeContainer().FinalEvent += StartFinalMotion;
        }

        private void StartFinalMotion()
        {
            
        }

        private void OnDisable()
        {
            pointController.GetUpgradeContainer().FinalEvent -= StartFinalMotion;
        }
    }
}