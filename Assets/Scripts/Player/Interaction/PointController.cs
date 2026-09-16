using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Upgrades;

namespace Interaction
{
    public class PointController : MonoBehaviour
    {
        [SerializeField] private float points = 0f;
        [SerializeField] private PointUiController pointUIController;
        private UpgradeContainer _upgradeContainer;
        private readonly PlayerStats _stats = new();
        private readonly WaitForSeconds _waitTime = new(1f);  // 1 second
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
        public float GetPoints() => points;
        public IReadOnlyList<UpgradeInstance> GetUpgrades() => _upgradeContainer.upgrades;
        public UpgradeContainer GetUpgradeContainer() => _upgradeContainer;
        public void SetPoints(float amount) => points = amount;
    }
}