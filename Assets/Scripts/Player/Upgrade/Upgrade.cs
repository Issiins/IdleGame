using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Upgrade
{
    // Runtime wrapper class
    public class Upgrade
    {
        public UpgradeData Data { get; }
        public int Level { get; private set; } 

        public Upgrade(UpgradeData data)
        {
            Data = data;
        }
        public void LevelUp() => Level++;
        public float CurrentCost => Data.Cost * (Level + 1);
        public bool IsPurchased() => Level > 0;
        
        /*Does not recursively check if required upgrades are themselves unlocked.
        * If recursive checking is added later, validate for circular dependencies first.
        */
        public bool IsUnlocked(IReadOnlyList<Upgrade> allUpgrades)
        {
            foreach (var requirement in Data.UnlockRequirements)
            {
                foreach (var upgrade in allUpgrades)
                {
                    if(requirement.Data != upgrade.Data)
                        continue;
                    if(upgrade.Level < requirement.Level)
                        return false;
                }
            }
            return true;
        }
    }
}
