using System.Collections;
using System.Collections.Generic;
using Player;
using UI.WordWaterfall;
using UnityEngine;
using Upgrades;

namespace Interaction
{
    public class PointController : MonoBehaviour
    {
        [SerializeField] private float points = 0f;
        [SerializeField] private PointUiController pointUIController;
        [SerializeField] private WordSpawner wordSpawner;
        private UpgradeContainer _upgradeContainer;
        private readonly PlayerStats _stats = new();
        private readonly WaitForSeconds _waitTime = new(1f);  // 1 second
        private const float InsanityLimit = 500f; 
        private void Awake()
        {
            UpgradeFactory upgradeFactory = new();
            _upgradeContainer = new UpgradeContainer(_stats, upgradeFactory.GetUpgrades(), pointUIController);
            StartCoroutine(Accumalate());
        }
        public void OnClick()
        {
            points += _stats.ClickMultiplier;
            pointUIController.UpdateValue();
        }
        private IEnumerator Accumalate()
        {
            while (true)
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
            var isOverLimit = points > InsanityLimit;
            switch (isOverLimit)
            {
                case false when wordSpawner.GetCurrentActivity():
                    wordSpawner.SetActive(false);
                    return;
                case true:
                    wordSpawner.SetActive(true);
                    break;
                default:
                    return;
            }
            switch (points)
            {
                case < 1000f:
                    wordSpawner.SetSpawnRate(12f);
                    break;
                case >= 1000f and < 5000f:
                    wordSpawner.SetSpawnRate(8f);
                    break;
                case >= 5000f and < 10000f:
                    wordSpawner.SetSpawnRate(3f);
                    break;
                case >= 10000f and 20000f:
                    wordSpawner.SetSpawnRate(1f);
                    break;
                case >= 20000f and < 40000f:
                    wordSpawner.SetSpawnRate(0.1f);
                    break;
            }
        }
        public float GetPoints() => points;
        public IReadOnlyList<UpgradeInstance> GetUpgrades() => _upgradeContainer.upgrades;
        public UpgradeContainer GetUpgradeContainer() => _upgradeContainer;
        public PointUiController GetPointUIController() => pointUIController;
        public void SetPoints(float amount) => points = amount;
    }
}