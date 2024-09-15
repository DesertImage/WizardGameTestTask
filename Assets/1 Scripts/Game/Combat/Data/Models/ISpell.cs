using GameCOP.Spawning;

namespace GameCOP.Combat
{
    public interface ISpell
    {
        public ObjectId PrefabId { get; }
        public float Damage { get; }
    }
}