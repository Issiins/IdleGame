using Unity.VisualScripting;

namespace Upgrade
{
    public class UnlockRequirement
    {
        public UpgradeData Data { get; }
        public int Level { get; }

        public UnlockRequirement(UpgradeData data, int level)
        {
            Data = data;
            Level = level;
        }
    }
}