using System;
using Player;

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
