using System;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Upgrades
{
    [Serializable]
    public class ProductionEffect : IEffect
    {
        private const float MultiplierPerLevel = 2f;
        public void Apply(byte level,  PlayerStats stats)
        {
            var bonus = level * MultiplierPerLevel;
            stats.AddProductionMultiplier(bonus);
        }
    }
}
