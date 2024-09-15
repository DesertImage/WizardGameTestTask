using System.Collections.Generic;

namespace GameCOP
{
    public abstract class PrototypePool<T> : IPrototypePool<T> where T : IPoolable
    {
        protected struct PoolData
        {
            public T Original;
            public Stack<T> Instances;
        }

        protected readonly IDictionary<int, PoolData> Instances = new Dictionary<int, PoolData>();

        public void Register(T original, int count)
        {
            var id = GetId(original);

            var poolData = new PoolData
            {
                Original = original,
                Instances = new Stack<T>(count)
            };

            for (var i = 0; i < count; i++)
            {
                var instance = CreateNew(original);

                OnRelease(instance, true);
                poolData.Instances.Push(instance);
            }

            Instances.Add(id, poolData);
        }

        public T GetNewInstance(T original, bool isSilently = false)
        {
            var poolData = Instances[GetId(original)];
            var instance = poolData.Instances.Count > 0 ? poolData.Instances.Pop() : CreateNew(poolData.Original);

            OnSpawn(original, instance, isSilently);

            return instance;
        }

        protected abstract T CreateNew(T original);

        public virtual void Release(T instance)
        {
            var id = GetId(instance);

            if (!Instances.TryGetValue(id, out var poolData)) return;

            OnRelease(instance);
            poolData.Instances.Push(instance);
        }

        protected abstract int GetId(T prototype);

        protected virtual void OnSpawn(T original, T instance, bool isSilently = false)
        {
            if (!isSilently)
            {
                instance.OnSpawn();
            }

            instance.ReleaseRequest += OnReleaseRequest;
        }

        protected virtual void OnRelease(T instance, bool isSilently = false)
        {
            instance.ReleaseRequest -= OnReleaseRequest;
            if(isSilently) return;
            instance.OnRelease();
        }

        protected void OnReleaseRequest(IPoolable instance) => Release((T)instance);
    }
}