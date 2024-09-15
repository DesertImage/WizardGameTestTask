using System;

namespace GameCOP
{
    public interface IEventsManager
    {
        void Listen<T>(IListener<T> listener) where T : struct;
        void Unlisten<T>(IListener<T> listener) where T : struct;

        void Send<T>() where T : struct;
        void Send<T>(T args) where T : struct;

        void Clear();
    }
}