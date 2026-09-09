namespace Player
{
    public class PlayerStats
    {
        public float ClickMultiplier { get; private set; }
        public float ProductionPerSecond { get; private set; }

        private const float BaseClickMultiplier = 1f;
        private const float BaseProductionPerSecond = 0f;

        public PlayerStats()
        {
            Reset();
        }
        public void Reset()
        {
            ClickMultiplier = BaseClickMultiplier;
            ProductionPerSecond = BaseProductionPerSecond;
        }

        public void AddClickMultiplier(float amount) => ClickMultiplier += amount;
        public void AddProductionMultiplier(float amount) => ProductionPerSecond += amount;
    }

    
}