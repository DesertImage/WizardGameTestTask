using System;
using System.Collections.Generic;

namespace GameCOP
{
    public class Pool<T> : IPool<T> where T : IPoolable
    {
        public event Action<T> Spawn;

        private readonly Stack<T> _instances;
        private readonly Func<T> _factory;

        public Pool(int preRegisteredCount, Func<T> factory)
        {
            _factory = factory;
            _instances = new Stack<T>();

            for (var i = 0; i < preRegisteredCount; i++)
            {
                _instances.Push(CreateNew());
            }
        }

        public T GetNewInstance(bool isSilently = false)
        {
            var instance = _instances.Count > 0 ? _instances.Pop() : CreateNew();

            instance.ReleaseRequest += ReleaseRequest;

            if (!isSilently)
            {
                instance.OnSpawn();
            }

            Spawn?.Invoke(instance);

            return instance;
        }


        public void Release(T poolable)
        {
            poolable.ReleaseRequest -= ReleaseRequest;
            
            poolable.OnRelease();
            _instances.Push(poolable);
        }

        private T CreateNew() => _factory.Invoke();

        private void ReleaseRequest(IPoolable obj) => Release((T)obj);
    }
}