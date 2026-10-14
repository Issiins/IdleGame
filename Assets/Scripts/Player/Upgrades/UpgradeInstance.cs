using System.Collections.Generic;

namespace Upgrades
{
    // Runtime wrapper class
    public class UpgradeInstance
    {
        public UpgradeData data { get; }
        public byte level { get; set; } 

        public UpgradeInstance(UpgradeData data)
        {
            this.data = data;
        }
        public void LevelUp() => level++;
        public bool isPurchased => level > 0;
        public int currentCost => data.cost + level * data.cost * data.priceIncrease;
        /// <summary>
        /// Checks level requirements if upgrade can be unlocked
        /// </summary>
        /// <param name="allUpgrades">List of all upgrades</param>
        /// <returns>returns true if level is above or equal to requirement</returns>
        public bool IsUnlockable(IReadOnlyList<UpgradeInstance> allUpgrades)
        {
            foreach (var requirement in data.unlockRequirements)
            {
                bool met = false;
                foreach (var upgrade in allUpgrades)
                {
                    if(requirement.Data != upgrade.data)
                        continue;
                    if(upgrade.level >= requirement.Level)
                        met = true;
                    break;
                }
                if(!met)
                    return false;
            }
            return true;
        }
        public static bool operator >(UpgradeInstance a, UpgradeInstance b) => a.data.cost > b.data.cost;
        public static bool operator <(UpgradeInstance a, UpgradeInstance b) =>  a.data.cost < b.data.cost;
    }
}
