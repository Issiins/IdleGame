using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Upgrades
{
    // Runtime wrapper class
    public class UpgradeInstance
    {
        public UpgradeData Data { get; }
        public byte Level { get; private set; } 

        public UpgradeInstance(UpgradeData data)
        {
            Data = data;
        }
        public void LevelUp() => Level++;
        public int CurrentCost => Data.Cost * (Level + 1);
        public bool IsPurchased() => Level > 0;
        /*Does not recursively check if required upgrades are themselves unlocked.
        * If recursive checking is added later, validate for circular dependencies first.
        */
        public bool IsUnlocked(IReadOnlyList<UpgradeInstance> allUpgrades)
        {
            foreach (var requirement in Data.UnlockRequirements)
            {
                foreach (var upgrade in allUpgrades)
                {
                    if(requirement.Data == upgrade.Data)
                        continue;
                    if(upgrade.Level < requirement.Level)
                        return false;
                }
            }
            return true;
        }
    }
}
