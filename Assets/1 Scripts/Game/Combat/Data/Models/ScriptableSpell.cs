using GameCOP.Spawning;
using UnityEngine;

namespace GameCOP.Combat
{
    [CreateAssetMenu(fileName = "Spell", menuName = "Game/Spell")]
    public class ScriptableSpell : ScriptableObject, ISpell
    {
        public ObjectId PrefabId => prefabId;
        public float Damage => damage;

        [SerializeField] private ObjectId prefabId;
        [SerializeField] private float damage;
    }
}