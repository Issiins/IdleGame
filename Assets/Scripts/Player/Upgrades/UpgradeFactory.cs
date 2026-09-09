using System.Collections.Generic;
using Player.Upgrade;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Upgrades
{
    /// <summary>
    /// Factory class to create upgrades dynamically from resources folder
    /// </summary>
    public class UpgradeFactory
    {
        private readonly UpgradeData[] _loadedData;
        private List<UpgradeInstance> _upgrades = new();

        public UpgradeFactory()
        {
            _loadedData = Resources.LoadAll<UpgradeData>("Upgrades"); // Loads all data from the folder "Assets/Resources/Upgrades"
            CreateUpgrades(_loadedData);
        }

        private void CreateUpgrades(UpgradeData[] data)
        {
            if(_loadedData == null)
                return;
            foreach (var curr in data)
                _upgrades.Add(new UpgradeInstance(curr));
            
        }
        public List<UpgradeInstance> GetUpgrades()
        {
            return _upgrades;
        }
        
    }
}