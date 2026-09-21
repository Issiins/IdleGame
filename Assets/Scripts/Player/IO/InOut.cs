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
                upgrades = _upgradeContainer.upgrades.Select(upgrade => new UpgradeDTO(upgrade.Data.Name, upgrade.Level)).ToArray()
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
            // only works if no new upgrades have been added
            for (var i = 0; i < data.upgrades.Length; i++)
            {
                if (_upgradeContainer.upgrades[i].Data.Name != data.upgrades[i].upgradeName) continue;
                bool levelOverCap = _upgradeContainer.upgrades[i].Data.MaxLevel < data.upgrades[i].upgradeLevel; // checks if max level was changed
                _upgradeContainer.upgrades[i].Level = levelOverCap ? 
                    _upgradeContainer.upgrades[i].Data.MaxLevel : 
                    data.upgrades[i].upgradeLevel;
            }
            _upgradeContainer.UpdateMultiplier();
        }
        private string GetFullPath(string fileName) => Path.Combine(PATH, fileName);
    }
}