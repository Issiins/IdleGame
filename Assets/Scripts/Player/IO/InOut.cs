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
        public void Save(PointController pointController, UpgradeContainer upgradeContainer)
        {
            SaveFileDTO saveFileDto = new()
            {
                points = pointController.GetPoints(),
                upgrades = upgradeContainer.upgrades.Select(upgrade => new UpgradeDTO(upgrade.Data.Name, upgrade.Level)).ToArray()
            };
            var jsonContent = JsonUtility.ToJson(saveFileDto);
            using var write = new StreamWriter(GetFullPath("save.json"));
            write.Write(jsonContent);
            write.Close();
        }
        public void Load(PointController pointController, UpgradeContainer upgradeContainer)
        {
            string jsonContent = File.ReadAllText(GetFullPath("save.json"));
            var data = JsonUtility.FromJson<SaveFileDTO>(jsonContent);
            pointController.SetPoints(data.points);
            // only works if no new upgrades have been added
            for (var i = 0; i < data.upgrades.Length; i++)
            {
                if (upgradeContainer.upgrades[i].Data.Name != data.upgrades[i].upgradeName) continue;
                bool levelOverCap = upgradeContainer.upgrades[i].Data.MaxLevel < data.upgrades[i].upgradeLevel; // checks if max level was changed
                upgradeContainer.upgrades[i].Level = levelOverCap ? 
                    upgradeContainer.upgrades[i].Data.MaxLevel : 
                    data.upgrades[i].upgradeLevel;
            }
            upgradeContainer.UpdateMultiplier();
        }
        private string GetFullPath(string fileName) => Path.Combine(PATH, fileName);
    }
}