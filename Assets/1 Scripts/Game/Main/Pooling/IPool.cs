using System;

namespace GameCOP
{
    public interface IPool<T> where T : IPoolable
    {
        event Action<T> Spawn;
        
        T GetNewInstance(bool isSilently = false);

        void Release(T poolable);
    }

    public interface IPrototypePool<T> where T : IPoolable
    {
        void Register(T original, int count);

        T GetNewInstance(T original, bool isSilently = false);

        void Release(T instance);
    }
}