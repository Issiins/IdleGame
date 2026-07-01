using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Upgrade
{
    public class Upgrade
    {
        public UpgradeData Data { get; }
        public int Level { get; private set; }
        
        public Upgrade(UpgradeData data)
        {
            Data = data;
            Level = 0;
        }

        public void LevelUp() => Level++;
        public float CurrentCost => Data.Cost * (Level + 1);

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
