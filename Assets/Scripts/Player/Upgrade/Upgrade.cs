using UnityEngine;

namespace Upgrade
{
    public class Upgrade
    {
        public UpgradeData Data { get; }
        public int Level { get; private set; }

        public Upgrade(UpgradeData data)
        {
            Data = data;
            Level = 0;
        }

        public void LevelUp() => Level++;
        public float CurrentCost => Data.Cost * (Level + 1);
    }
}
