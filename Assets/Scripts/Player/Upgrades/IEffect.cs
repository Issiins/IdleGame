using Player;

namespace Upgrades
{
    public interface IEffect
    {
        void Apply(int level, PlayerStats stats);
    }
}
