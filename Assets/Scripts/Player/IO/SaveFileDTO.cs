using System;

namespace Player
{
    [Serializable]
    public class SaveFileDTO
    {
        public float points;
        public UpgradeDTO[] upgrades;
    }
}