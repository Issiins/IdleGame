using System;
using Unity.VisualScripting;

namespace Upgrade
{
    [Serializable]
    public class UnlockRequirement
    {
        public UpgradeData Data;
        public int Level;

        public UnlockRequirement(UpgradeData data, int level)
        {
            Data = data;
            Level = level;
        }
    }
}