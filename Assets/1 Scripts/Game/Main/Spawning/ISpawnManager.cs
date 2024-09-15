using UnityEngine;

namespace GameCOP.Spawning
{
    public interface ISpawnManager
    {
        void Register(int id, MonoPoolable original, int preRegisteredCount = 1);
        T Spawn<T>(int id) where T : MonoPoolable;
        T Spawn<T>(int id, Vector3 position) where T : MonoPoolable;
        T Spawn<T>(int id, Vector3 position, Quaternion rotation) where T : MonoPoolable;
    }
}