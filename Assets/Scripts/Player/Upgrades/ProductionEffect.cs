using System;
using Player;

namespace Upgrades
{
    [Serializable]
    public class ProductionEffect : IEffect
    {
        public void Apply(byte level,  PlayerStats stats, float multiplierPerLevel)
        {
            var bonus = level * multiplierPerLevel;
            stats.AddProductionMultiplier(bonus);
        }
    }
}
