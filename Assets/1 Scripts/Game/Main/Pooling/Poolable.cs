using System;

namespace GameCOP
{
    public abstract class Poolable : IPoolable
    {
        public event Action<IPoolable> ReleaseRequest;

        public abstract void OnSpawn();
        public abstract void OnRelease();

        public void Release() => ReleaseRequest?.Invoke(this);
    }
}