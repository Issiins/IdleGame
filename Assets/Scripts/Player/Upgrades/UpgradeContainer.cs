using System;
using System.Collections.Generic;
using Interaction;
using Player;
using UnityEngine;

namespace Upgrades
{
    /// <summary>
    /// Upgrade data container, saves all upgrades 
    /// </summary>
    public class UpgradeContainer
    {
        private List<UpgradeInstance> _upgrades = new();
        private readonly PlayerStats _stats;
        public List<UpgradeInstance> upgrades => _upgrades;
        public event Action OnUpgradePurchased;
        public UpgradeContainer(PlayerStats stats, List<UpgradeInstance> upgrades)
        {
            _stats = stats;
            foreach (var upgrade in upgrades)
            {
                _upgrades.Add(upgrade);
            }
        }
        public void UpdateMultiplier()
        {
            _stats.Reset();
            foreach (var upgrade in _upgrades)
            {
                upgrade.Data.Effect.Apply(upgrade.Level, _stats);
            }
        }

        public bool TryPurchase(UpgradeInstance upgrade, PointController pointController)
        {
            bool success = pointController.TrySpendPoints(upgrade.CurrentCost);
            if(!success) return false;  
            upgrade.LevelUp();
            UpdateMultiplier();
            OnUpgradePurchased?.Invoke();
            return true;
        }
    }
}