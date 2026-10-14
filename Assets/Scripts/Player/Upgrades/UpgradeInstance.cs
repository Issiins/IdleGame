using System.Collections.Generic;

namespace Upgrades
{
    // Runtime wrapper class
    public class UpgradeInstance
    {
        public UpgradeData Data { get; }
        public byte Level { get; set; } 

        public UpgradeInstance(UpgradeData data)
        {
            Data = data;
        }
        public void LevelUp() => Level++;
        public bool IsPurchased => Level > 0;
        public int CurrentCost => Data.cost + Level * Data.cost * Data.priceIncrease;
        /// <summary>
        /// Checks level requirements if upgrade can be unlocked
        /// </summary>
        /// <param name="allUpgrades">List of all upgrades</param>
        /// <returns>returns true if level is above or equal to requirement</returns>
        public bool IsUnlockable(IReadOnlyList<UpgradeInstance> allUpgrades)
        {
            foreach (var requirement in Data.unlockRequirements)
            {
                bool met = false;
                foreach (var upgrade in allUpgrades)
                {
                    if(requirement.Data != upgrade.Data)
                        continue;
                    if(upgrade.Level >= requirement.Level)
                        met = true;
                    break;
                }
                if(!met)
                    return false;
            }
            return true;
        }
        public static bool operator >(UpgradeInstance a, UpgradeInstance b) => a.Data.cost > b.Data.cost;
        public static bool operator <(UpgradeInstance a, UpgradeInstance b) =>  a.Data.cost < b.Data.cost;
    }
}
