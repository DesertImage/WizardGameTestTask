using System;

namespace GameCOP
{
    public interface IPoolable
    {
        event Action<IPoolable> ReleaseRequest;

        void OnSpawn();
        void OnRelease();

        void Release();
    }
}