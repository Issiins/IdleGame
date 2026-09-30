using System;

namespace Player
{
    [Serializable]
    public class UpgradeDTO
    {
        public string upgradeName;
        public byte upgradeLevel;
        public UpgradeDTO(string  upgradeName, byte upgradeLevel)
        {
            this.upgradeName = upgradeName;
            this.upgradeLevel = upgradeLevel;
        }
    }
}