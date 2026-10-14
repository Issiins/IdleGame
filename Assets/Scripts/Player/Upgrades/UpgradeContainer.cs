using System;
using System.Collections.Generic;
using Interaction;
using Player;

namespace Upgrades
{
    /// <summary>
    /// Upgrade data container, saves all upgrades 
    /// </summary>
    public class UpgradeContainer
    {
        private readonly PointUiController _pointUiController;
        private List<UpgradeInstance> _upgrades = new();
        private readonly PlayerStats _stats;
        public List<UpgradeInstance> upgrades => _upgrades;
        public event Action OnUpgradePurchased;
        public event Action FinalEvent;
        private const string FinalUpgradeName = "Death"; 
        public UpgradeContainer(PlayerStats stats, List<UpgradeInstance> upgrades,  PointUiController pointUiController)
        {
            _stats = stats;
            foreach (var upgrade in upgrades)
            {
                _upgrades.Add(upgrade);
            }
            ReverseSort();
            _pointUiController = pointUiController;
        }
        public void UpdateMultiplier()
        {
            _stats.Reset();
            foreach (var upgrade in _upgrades)
            {
                upgrade.Data.effect.Apply(upgrade.Level, _stats, upgrade.Data.multiplier);
                if (!upgrade.Data.name.Equals(FinalUpgradeName) || !upgrade.IsPurchased) continue;
                FinalEvent?.Invoke();
            }
        }
        public void TryPurchase(UpgradeInstance upgrade, PointController pointController)
        {
            var success = pointController.TrySpendPoints(upgrade.CurrentCost);
            if(!success) return;
            _pointUiController.UpdateValue();
            upgrade.LevelUp();
            UpdateMultiplier();
            OnUpgradePurchased?.Invoke();
        }
        private void ReverseSort()
        {
            var i = 1;
            while (i < _upgrades.Count)
            {
                var temp = _upgrades[i];
                var j = i;
                while (j > 0 && _upgrades[j - 1] < temp)
                {
                    _upgrades[j] = _upgrades[j - 1];
                    j--;
                }
                _upgrades[j] = temp;
                i++;
            }
        }
    }
}