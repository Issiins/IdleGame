namespace Upgrade
{
    public class UpgradeData
    {
        public string Name { get; }
        public string Description { get; }
        public float Cost { get; }
        public int Level { get; private set; }

        public UpgradeData(string name, string description, float cost)
        {
            Name = name;
            Description = description;
            Cost = cost;
            Level = 1;
        }

        public void LevelUp() => Level++;
    }
}
