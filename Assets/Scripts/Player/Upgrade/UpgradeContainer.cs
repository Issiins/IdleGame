using System.Collections.Generic;
using Player.Upgrade;

namespace Upgrade
{
    /// <summary>
    /// Upgrade data container, saves all upgrades 
    /// </summary>
    public class UpgradeContainer
    {
        private List<Upgrade> _upgrades = new();
        private readonly PlayerStats _stats;

        public UpgradeContainer(PlayerStats stats)
        {
            _stats = stats;
        }
        public void AddUpgrade(Upgrade upgrade) => _upgrades.Add(upgrade);
        public void UpdateMultiplier()
        {
            _stats.Reset();
            foreach (var upgrade in _upgrades)
            {
                upgrade.Data.Effect.Apply(upgrade.Data.Level, _stats);
            }
        }
    }
}