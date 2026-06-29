using Player.Upgrade;

namespace Upgrade
{
    public interface IEffect
    {
        void Apply(int level, PlayerStats stats);
    }
}
