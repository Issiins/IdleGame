using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interaction;
using UI;
using UnityEngine;
using Upgrades;

namespace Player
{
    // TODO make it load file with new changes. Add response in the UI when the game is saved and loaded
    public class InOut
    {
        private static readonly string PATH = Application.persistentDataPath;
        private static readonly string FILENAME = "save.json";
        private readonly PointController _pointController;
        private readonly UpgradeContainer _upgradeContainer;
        private readonly Message _message;
        public InOut(PointController pointController, UpgradeContainer upgradeContainer, Message message)
        {
            _pointController = pointController;
            _upgradeContainer = upgradeContainer;
            _message = message;
        }
        public void Save()
        {
            SaveFileDTO saveFileDto = new()
            {
                points = _pointController.GetPoints(),
                upgrades = _upgradeContainer.upgrades.Select(upgrade => 
                    new UpgradeDTO(upgrade.data.name, upgrade.level)).ToArray()
            };
            var jsonContent = JsonUtility.ToJson(saveFileDto);
            using var write = new StreamWriter(GetFullPath(FILENAME));
            write.Write(jsonContent);
            write.Close();
            _message.ShowMessage("Saved Successfully!");
        }
        public void Load()
        {
            string jsonContent = File.ReadAllText(GetFullPath(FILENAME));
            var data = JsonUtility.FromJson<SaveFileDTO>(jsonContent);
            _pointController.SetPoints(data.points);
            var upgradeByName = new Dictionary<string, UpgradeInstance>(_upgradeContainer.upgrades.Count); 
            foreach(var upgrade in _upgradeContainer.upgrades)
                upgradeByName[upgrade.data.name] = upgrade;
            foreach (var savedUpgrade in data.upgrades)
            {
                if (!upgradeByName.TryGetValue(savedUpgrade.upgradeName, out var upgrade))
                    continue; 
                bool levelOverCap = upgrade.data.maxLevel < savedUpgrade.upgradeLevel; // checks if max level was changed
                upgrade.level = levelOverCap ? upgrade.data.maxLevel : savedUpgrade.upgradeLevel;
            }
            _upgradeContainer.UpdateMultiplier();
            _pointController.GetPointUIController().UpdateValue();
            _message.ShowMessage("Loaded Successfully!");
        }
        private string GetFullPath(string fileName) => Path.Combine(PATH, fileName);
    }
}