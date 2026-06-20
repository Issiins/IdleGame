using UnityEngine;

namespace Upgrade
{
    public class ClickEffect : IEffect
    {
        private readonly float _multiplierPerLevel;

        public ClickEffect(float multiplierPerLevel)
        {
            _multiplierPerLevel = multiplierPerLevel;
        }

        public void Apply(int level)
        {
            float bonus = _multiplierPerLevel * level;
            Debug.Log($"Click value boosted by {bonus}");
        }
    }
}
