using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interaction;
using UnityEngine;
using Upgrades;

namespace Player
{
    // TODO make it load file with new changes. Add response in the UI when the game is saved and loaded
    public class InOut
    {
        private static readonly string PATH = Application.persistentDataPath;
        private readonly PointController _pointController;
        private readonly UpgradeContainer _upgradeContainer;
        public InOut(PointController pointController, UpgradeContainer upgradeContainer)
        {
            _pointController = pointController;
            _upgradeContainer = upgradeContainer;
        }
        public void Save()
        {
            SaveFileDTO saveFileDto = new()
            {
                points = _pointController.GetPoints(),
                upgrades = _upgradeContainer.upgrades.Select(upgrade => new UpgradeDTO(upgrade.Data.name, upgrade.Level)).ToArray()
            };
            var jsonContent = JsonUtility.ToJson(saveFileDto);
            using var write = new StreamWriter(GetFullPath("save.json"));
            write.Write(jsonContent);
            write.Close();
        }
        public void Load()
        {
            string jsonContent = File.ReadAllText(GetFullPath("save.json"));
            var data = JsonUtility.FromJson<SaveFileDTO>(jsonContent);
            _pointController.SetPoints(data.points);
            var upgradeByName = new Dictionary<string, UpgradeInstance>(_upgradeContainer.upgrades.Count); 
            foreach(var upgrade in _upgradeContainer.upgrades)
                upgradeByName[upgrade.Data.name] = upgrade;
            foreach (var savedUpgrade in data.upgrades)
            {
                if (!upgradeByName.TryGetValue(savedUpgrade.upgradeName, out var upgrade))
                    continue; 
                bool levelOverCap = upgrade.Data.maxLevel < savedUpgrade.upgradeLevel; // checks if max level was changed
                    upgrade.Level = levelOverCap ? 
                    upgrade.Data.maxLevel : 
                    savedUpgrade.upgradeLevel;
            }
            _upgradeContainer.UpdateMultiplier();
            _pointController.GetPointUIController().UpdateValue();
        }
        private string GetFullPath(string fileName) => Path.Combine(PATH, fileName);
    }
}