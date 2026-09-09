using System;
using Player;
using UnityEngine;

namespace Upgrades
{
    [Serializable]
    public class ClickEffect : IEffect
    {
        private const float MultiplierPerLevel = 2f;
        public void Apply(byte level, PlayerStats stats)
        {
            stats.AddClickMultiplier(MultiplierPerLevel * level);
        }
    }
}
