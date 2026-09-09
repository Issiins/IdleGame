using Player;

namespace Upgrades
{
    public interface IEffect
    {
        void Apply(byte level, PlayerStats stats);
    }
}
