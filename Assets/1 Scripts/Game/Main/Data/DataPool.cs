using System;
using System.Collections.Generic;

namespace GameCOP
{
    public class DataPool
    {
        private readonly Dictionary<Type, Stack<object>> _data = new Dictionary<Type, Stack<object>>();

        public void Register<T>(int preRegisterCount = 1) where T : IPoolable, new()
        {
            var instances = new Stack<object>();

            for (var i = 0; i < preRegisterCount; i++)
            {
                instances.Push(CreateNew<T>());
            }

            _data.Add(typeof(T), instances);
        }

        public T GetNewInstance<T>() where T : IPoolable, new()
        {
            var type = typeof(T);

            T instance;

            if (_data.TryGetValue(type, out var instances))
            {
                instance = instances.Count > 0 ? (T)instances.Pop() : (T)CreateNew<T>();
            }
            else
            {
                instance = (T)CreateNew<T>();
            }

            OnSpawn(instance);

            return instance;
        }

        public void Release<T>(T instance) where T : IPoolable
        {
            OnRelease(instance);
            _data[typeof(T)].Push(instance);
        }

        private static object CreateNew<T>() where T : IPoolable, new() => new T();

        private static void OnSpawn<T>(T instance) where T : IPoolable
        {
            instance.ReleaseRequest += ReleaseRequest;
            instance.OnSpawn();
        }

        private static void OnRelease<T>(T instance) where T : IPoolable
        {
            instance.ReleaseRequest -= ReleaseRequest;
            instance.OnRelease();
        }

        private static void ReleaseRequest<T>(T instance) where T : IPoolable => OnRelease(instance);
    }
}