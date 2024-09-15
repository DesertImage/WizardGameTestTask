using System;
using System.Collections.Generic;

namespace GameCOP
{
    public class EventsManager : IEventsManager
    {
        private readonly Dictionary<Type, List<IListener>> _listeners = new Dictionary<Type, List<IListener>>();

        public void Listen<T>(IListener<T> listener) where T : struct
        {
            var type = typeof(T);

            if (_listeners.TryGetValue(type, out var listeners))
            {
                listeners.Add(listener);
            }
            else
            {
                _listeners.Add(type, new List<IListener> { listener });
            }
        }

        public void Unlisten<T>(IListener<T> listener) where T : struct
        {
            if (!_listeners.TryGetValue(typeof(T), out var listeners)) return;
            listeners.Remove(listener);
        }

        public void Send<T>() where T : struct => Send(new T());

        public void Send<T>(T args) where T : struct
        {
            if (!_listeners.TryGetValue(typeof(T), out var listeners)) return;
            for (var i = 0; i < listeners.Count; i++)
            {
                ((IListener<T>)listeners[i]).HandleEvent(args);
            }
        }

        public void Clear() => _listeners.Clear();
    }
}