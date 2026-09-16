using System;
using System.Collections.Generic;

namespace Player
{
    [Serializable]
    public class SaveFileDTO
    {
        public float points;
        public UpgradeDTO[] upgrades;
    }
}