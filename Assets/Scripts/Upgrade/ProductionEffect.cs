using UnityEngine;

namespace Upgrade
{
    public class ProductionEffect : IEffect
    {
        private readonly float _multiplierPerLevel;

        public ProductionEffect(float multiplierPerLevel)
        {
            _multiplierPerLevel = multiplierPerLevel;
        }

        public void Apply(int level)
        {
            float bonus = _multiplierPerLevel * level;
        }
    }
}
