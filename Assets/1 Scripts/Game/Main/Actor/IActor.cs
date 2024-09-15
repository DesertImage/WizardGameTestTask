namespace GameCOP
{
    public interface IActor : IUpdatable, IPoolable, IEventsManager, IInjectable<IServiceLocator>
    {
        T Add<T>(T module) where T : IData;
        T Add<T>() where T : IData, new();
        void AddBehaviour<T>() where T : IBehaviour, new();

        T Get<T>() where T : IData;

        bool Has<T>() where T : IData;
        
        void Remove<T>();

        void ListenGlobal<T>(IListener<T> listener) where T : struct;
        void UnlistenGlobal<T>(IListener<T> listener) where T : struct;

        void SendGlobal<T>() where T : struct;
        void SendGlobal<T>(T args) where T : struct;
    }
}