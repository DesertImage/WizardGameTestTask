using System;
using System.Collections.Generic;

namespace GameCOP
{
    public class Actor : Poolable, IActor
    {
        private readonly Dictionary<Type, IData> _data = new Dictionary<Type, IData>();

        private readonly List<IUpdatable> _updatables = new List<IUpdatable>();
        private readonly Dictionary<Type, IBehaviour> _behaviours = new Dictionary<Type, IBehaviour>();

        private IServiceLocator _services;
        private ActorsManager _actorsManager;

        private IEventsManager _globalEventsManager = new EventsManager();
        private readonly IEventsManager _localEventsManager = new EventsManager();

        public void Inject(IServiceLocator services)
        {
            _services = services;
            _actorsManager = _services.Get<ActorsManager>();
            _globalEventsManager = services.Get<EventsManager>();
        }

        public T Add<T>() where T : IData, new() => Add((T)_actorsManager.GetDataInstance<T>());

        public T Add<T>(T module) where T : IData
        {
            _data.Add(typeof(T), module);
            return module;
        }

        public void AddBehaviour<T>() where T : IBehaviour, new()
        {
            AddBehaviour(typeof(T), _actorsManager.GetBehaviourInstance<T>());
        }

        public T Get<T>() where T : IData => (T)_data[typeof(T)];

        public bool Has<T>() where T : IData => _data.ContainsKey(typeof(T));

        public void Remove<T>()
        {
            var type = typeof(T);

            if (_data.ContainsKey(type))
            {
                _data[type].Release();
                _data.Remove(type);
            }
            else
            {
                _behaviours[type].Release();
                _behaviours.Remove(type);
            }
        }

        private void AddBehaviour(Type type, IBehaviour behaviour)
        {
            if (behaviour is IAwakable awakable)
            {
                awakable.OnAwake(_services);
            }

            if (behaviour is IUpdatable updatable)
            {
                _updatables.Add(updatable);
            }

            _behaviours.Add(type, behaviour);

            behaviour.Inject(_globalEventsManager);
            behaviour.Bind(this);
            behaviour.Activate();
        }

        public void Update(float deltaTime)
        {
            for (var i = 0; i < _updatables.Count; i++)
            {
                _updatables[i].Update(deltaTime);
            }
        }

        #region EVENTS

        public void Listen<T>(IListener<T> listener) where T : struct => _localEventsManager.Listen<T>(listener);

        public void ListenGlobal<T>(IListener<T> listener) where T : struct
        {
            _globalEventsManager.Listen(listener);
        }

        public void Unlisten<T>(IListener<T> listener) where T : struct => _localEventsManager.Unlisten<T>(listener);

        public void UnlistenGlobal<T>(IListener<T> listener) where T : struct
        {
            _globalEventsManager.Unlisten<T>(listener);
        }

        public void Clear() => _localEventsManager.Clear();

        public void Send<T>() where T : struct => Send(new T());
        public void SendGlobal<T>() where T : struct => SendGlobal(new T());
        public void Send<T>(T data) where T : struct => _localEventsManager.Send(data);
        public void SendGlobal<T>(T data) where T : struct => _globalEventsManager.Send(data);

        #endregion

        public override void OnSpawn()
        {
        }

        public override void OnRelease()
        {
            foreach (var pair in _behaviours)
            {
                pair.Value.Release();
            }

            _behaviours.Clear();

            foreach (var pair in _data)
            {
                pair.Value.Release();
            }

            _data.Clear();

            _localEventsManager.Clear();
            _updatables.Clear();
        }
    }
}