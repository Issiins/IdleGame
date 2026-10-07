using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Upgrades
{
    [Serializable]
    public class UnlockRequirement
    {
        public UpgradeData Data;
        public int Level;

        public UnlockRequirement(UpgradeData data, int level)
        {
            Data = data;
            Level = data != null ? Mathf.Clamp(level, 0, data.maxLevel) : level;
        }
    }
}