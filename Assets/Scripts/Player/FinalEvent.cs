using System;
using System.Collections;
using Interaction;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class FinalEvent : MonoBehaviour
    {
        [SerializeField] PointController pointController;
        [SerializeField] private float pointUpdateDuration;

        [Header("Random number range")] 
        [SerializeField] private float min;
        [SerializeField] private float max;
        private WaitForSeconds _wt;
        private Coroutine _coroutine;
        private void Awake()
        {
            pointController.GetUpgradeContainer().FinalEvent += StartFinalMotion;
            _wt = new WaitForSeconds(pointUpdateDuration);
        }

        private void StartFinalMotion()
        {
            pointController.ChangeRunningStatus(false);
            _coroutine = StartCoroutine(ShowRandomNumbers());
        }

        private IEnumerator ShowRandomNumbers()
        {
            while (true)
            {
                float randomNumber = Random.Range(min, max);
                pointController.GetPointUIController().UpdateValue(randomNumber);
                yield return _wt; 
            }
        }
        private void OnDisable()
        {
            pointController.GetUpgradeContainer().FinalEvent -= StartFinalMotion;
            if(_coroutine == null) return;
            StopCoroutine(_coroutine);
        }
    }
}