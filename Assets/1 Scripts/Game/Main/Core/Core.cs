using System;
using System.Collections.Generic;

namespace GameCOP
{
    public class Core : IServiceLocator, IAwakable, IStartable, IUpdatable, IDestroyable, IEventsManager
    {
        private readonly Dictionary<Type, object> _instances = new Dictionary<Type, object>();

        private readonly List<IAwakable> _awakables = new List<IAwakable>();
        private readonly List<IStartable> _startables = new List<IStartable>();
        private readonly List<IDestroyable> _destroyables = new List<IDestroyable>();

        private readonly UpdateManager _updateManager;
        private readonly IEventsManager _eventsManager;

        public Core()
        {
            _updateManager = new UpdateManager();
            _instances.Add(typeof(UpdateManager), _updateManager);

            _eventsManager = new EventsManager();
            _instances.Add(typeof(EventsManager), _eventsManager);
        }

        public void OnAwake(IServiceLocator services)
        {
            for (var i = 0; i < _awakables.Count; i++)
            {
                _awakables[i].OnAwake(this);
            }
        }

        public void OnStart()
        {
            for (var i = 0; i < _startables.Count; i++)
            {
                _startables[i].OnStart();
            }
        }

        public void OnDestroy()
        {
            for (var i = 0; i < _destroyables.Count; i++)
            {
                _destroyables[i].OnDestroy();
            }
        }

        public void Update(float deltaTime) => _updateManager.Update(deltaTime);

        public T Get<T>()
        {
            _instances.TryGetValue(typeof(T), out var instance);
            return (T)instance;
        }

        public void Add<T>() where T : new() => Add(new T());

        public void Add<T>(T service)
        {
            if (service is IInjectable<IServiceLocator> injectServices)
            {
                injectServices.Inject(this);
            }

            if (service is IAwakable awakable)
            {
                _awakables.Add(awakable);
            }

            if (service is IStartable startable)
            {
                _startables.Add(startable);
            }

            if (service is IDestroyable destroyable)
            {
                _destroyables.Add(destroyable);
            }

            if (service is IUpdatable updatable)
            {
                _updateManager.Add(updatable);
            }

            if (service is not IInitOnly)
            {
                _instances.Add(typeof(T), service);
            }
        }

        public void Remove<T>()
        {
            var type = typeof(T);

            _instances.TryGetValue(type, out var service);

            if (service is IAwakable awakable)
            {
                _awakables.Add(awakable);
            }

            if (service is IStartable startable)
            {
                _startables.Add(startable);
            }

            if (service is IDestroyable destroyable)
            {
                _destroyables.Add(destroyable);
            }

            if (service is IUpdatable updatable)
            {
                _updateManager.Add(updatable);
            }

            _instances.Remove(type);
        }

        public void Listen<T>(IListener<T> listener) where T : struct => _eventsManager.Listen<T>(listener);
        public void Unlisten<T>(IListener<T> listener) where T : struct => _eventsManager.Unlisten<T>(listener);

        public void Send<T>() where T : struct => Send(new T());
        public void Send<T>(T args) where T : struct => _eventsManager.Send(args);

        public void Clear() => _eventsManager.Clear();
    }
}