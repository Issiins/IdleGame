using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UI;
using UI.WordWaterfall;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Upgrades;

namespace Interaction
{
    public class PointController : MonoBehaviour
    {
        [Serializable]
        private struct InsanityValue
        {
            [Header("Range where this applies")] 
            public float min;
            public float max;
            [Header("Heartbeat values")]
            public float pauseDuration;
            public float pulseDuration;
            public float spawnRate;
        }
        [SerializeField] private float points = 0f;
        [Header("Controller references")]
        [SerializeField] private PointUiController pointUIController;
        [SerializeField] private WordSpawner wordSpawner;
        [SerializeField] private Heartbeat heart;
        [Header("Word spawner dynamical values")]
        [SerializeField] private InsanityValue defaultInsanityValue;
        [SerializeField] private List<InsanityValue> insanityValuesList;
        private UpgradeContainer _upgradeContainer;
        private readonly PlayerStats _stats = new();
        private readonly WaitForSeconds _waitTime = new(1f);  // 1 second
        private bool _isRunning = true;
        private void Awake()
        {
            UpgradeFactory upgradeFactory = new();
            _upgradeContainer = new UpgradeContainer(_stats, upgradeFactory.GetUpgrades(), pointUIController);
            StartCoroutine(Accumalate());
        }
        public void OnClick()
        {
            if(!_isRunning) return;
            points += _stats.ClickMultiplier;
            pointUIController.UpdateValue();
        }
        private IEnumerator Accumalate()
        {
            while (_isRunning)
            {
                InsanityCheck();
                points += _stats.ProductionPerSecond;
                pointUIController.UpdateValue();
                yield return _waitTime;   
            }
        }

        public bool TrySpendPoints(float amount)
        {
            if(amount > points) 
                return false;
            points -= amount;
            return true;
        }

        private void InsanityCheck()
        {
            if (defaultInsanityValue.min > points && points <= defaultInsanityValue.max)
            {
                wordSpawner.SetActive(false);
                heart.SetPauseDuration(defaultInsanityValue.pauseDuration);
                heart.SetPulseDuration(defaultInsanityValue.pulseDuration);
                return;
            }
            wordSpawner.SetActive(true);
            foreach (var currentCheck in insanityValuesList)
            {
                if (points >= currentCheck.min && points < currentCheck.max)
                {
                    heart.SetPauseDuration(currentCheck.pauseDuration);
                    heart.SetPulseDuration(currentCheck.pulseDuration);
                    wordSpawner.SetSpawnRate(currentCheck.spawnRate);
                }
            }
        }
        public float GetPoints() => points;
        public IReadOnlyList<UpgradeInstance> GetUpgrades() => _upgradeContainer.upgrades;
        public UpgradeContainer GetUpgradeContainer() => _upgradeContainer;
        public PointUiController GetPointUIController() => pointUIController;
        public void SetPoints(float amount) => points = amount;
        public void ChangeRunningStatus(bool isRunning) => _isRunning = isRunning;
    }
}