using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCOP.Spawning
{
    public class SpawnManager : ISpawnManager
    {
        public Action<IPoolable> OnPreSpawn = default;

        private readonly IDictionary<int, MonoPoolable> _data = new Dictionary<int, MonoPoolable>();

        private readonly IPrototypePool<MonoPoolable> _monoPool = new MonoPool();

        public void Register(int id, MonoPoolable original, int preRegisteredCount = 1)
        {
            _monoPool.Register(original, preRegisteredCount);
            _data.Add(id, original);
        }

        public T Spawn<T>(int id) where T : MonoPoolable
        {
            return SpawnProcess<T>(id, Vector3.zero, Quaternion.identity);
        }

        public T Spawn<T>(int id, Vector3 position) where T : MonoPoolable
        {
            return SpawnProcess<T>(id, position, Quaternion.identity);
        }

        public T Spawn<T>(int id, Vector3 position, Quaternion rotation) where T : MonoPoolable
        {
            return SpawnProcess<T>(id, position, rotation);
        }

        private T SpawnProcess<T>(int id, Vector3 position, Quaternion rotation) where T : MonoPoolable
        {
            var instance = _monoPool.GetNewInstance(_data[id], true);

            OnPreSpawn?.Invoke(instance);

            var transform = instance.transform;
            transform.position = position;
            transform.rotation = rotation;

            instance.gameObject.SetActive(true);
            instance.OnSpawn();

            return (T)instance;
        }
    }
}