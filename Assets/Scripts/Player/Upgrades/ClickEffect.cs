using System;
using Player;
using UnityEngine;

namespace Upgrades
{
    [Serializable]
    public class ClickEffect : IEffect
    {
        public void Apply(byte level, PlayerStats stats, float multiplierPerLevel)
        {
            stats.AddClickMultiplier(level * multiplierPerLevel);
        }
    }
}
