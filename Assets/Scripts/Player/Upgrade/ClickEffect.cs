using System;
using Player.Upgrade;
using UnityEngine;

namespace Upgrade
{
    [Serializable]
    public class ClickEffect : IEffect
    {
        public float _multiplierPerLevel;
        public void Apply(int level, PlayerStats stats)
        {
            stats.AddClickMultiplier(_multiplierPerLevel * level);
        }
    }
}
