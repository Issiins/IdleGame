using System.Collections.Generic;
using Player.Upgrade;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Upgrade
{
    public class UpgradeFactory
    {
        private readonly UpgradeData[] _loadedData;
        private List<Upgrade> _upgrades = new();

        public UpgradeFactory()
        {
            _loadedData = Resources.LoadAll<UpgradeData>("Upgrades");
            CreateUpgrades(_loadedData);
        }

        private void CreateUpgrades(UpgradeData[] data)
        {
            if(_loadedData == null)
                return;
            foreach (var curr in data)
                _upgrades.Add(new Upgrade(curr));
            
        }
        public List<Upgrade> GetUpgrades()
        {
            return _upgrades;
        }
        
    }
}