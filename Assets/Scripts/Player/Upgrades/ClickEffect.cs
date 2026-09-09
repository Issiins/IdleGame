using System;
using Player;
using UnityEngine;

namespace Upgrades
{
    [Serializable]
    public class ClickEffect : IEffect
    {
        private float _multiplierPerLevel = 2f;
        public void Apply(int level, PlayerStats stats)
        {
            stats.AddClickMultiplier(_multiplierPerLevel * level);
        }
    }
}
