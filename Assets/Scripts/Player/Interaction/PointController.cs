using System.Collections;
using System.Collections.Generic;
using Player;
using UI;
using UI.WordWaterfall;
using UnityEngine;
using Upgrades;

namespace Interaction
{
    public class PointController : MonoBehaviour
    {
        [SerializeField] private float points = 0f;
        [Header("Controller references")]
        [SerializeField] private PointUiController pointUIController;
        [SerializeField] private WordSpawner wordSpawner;
        [SerializeField] private Heartbeat heart;
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
                    heart.SetPauseDuration(duration: 1.5f);
                    return;
            }
            switch (points)
            {
                case < 1000f:
                    heart.SetPauseDuration(duration: 0.9f);
                    heart.SetPulseDuration(duration: 0.2f);
                    wordSpawner.SetSpawnRate(rate: 8f);
                    break;
                case >= 1000f and < 5000f:
                    heart.SetPauseDuration(duration: 0.7f);
                    heart.SetPulseDuration(duration: 0.2f);
                    wordSpawner.SetSpawnRate(rate: 4f);
                    break;
                case >= 5000f and < 10000f:
                    heart.SetPauseDuration(duration: 0.5f);
                    heart.SetPulseDuration(duration: 0.2f);
                    wordSpawner.SetSpawnRate(rate: 1f);
                    break;
                case >= 10000f and < 20000f:
                    heart.SetPauseDuration(duration: 0.3f);
                    heart.SetPulseDuration(duration: 0.2f);
                    wordSpawner.SetSpawnRate(rate: 0.75f);
                    break;
                case >= 20000f and < 40000f:
                    heart.SetPauseDuration(duration: 0.15f);
                    heart.SetPulseDuration(duration: 0.1f);
                    wordSpawner.SetSpawnRate(rate: 0.1f);
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