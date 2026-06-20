namespace Upgrade
{
    public class Upgrade
    {
        public UpgradeData Data { get; }
        public IEffect Effect { get; }

        public Upgrade(UpgradeData data, IEffect effect)
        {
            Data = data;
            Effect = effect;
        }

        public void Apply() => Effect.Apply(Data.Level);
    }
}
