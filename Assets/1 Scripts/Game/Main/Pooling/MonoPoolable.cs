using System;
using UnityEngine;

namespace GameCOP
{
    public abstract class MonoPoolable : MonoBehaviour, IPoolable
    {
        public event Action<IPoolable> ReleaseRequest;

        public abstract void OnSpawn();
        public abstract void OnRelease();

        public void Release() => ReleaseRequest?.Invoke(this);
    }
}