using System;
using Player.Upgrade;
using UnityEngine;

namespace Upgrade
{
    [Serializable]
    public class ProductionEffect : IEffect
    {
        private readonly float _multiplierPerLevel;
        public void Apply(int level,  PlayerStats stats)
        {
            stats.AddProduction(_multiplierPerLevel * level);
        }
    }
}
