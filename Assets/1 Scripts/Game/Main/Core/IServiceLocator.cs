namespace GameCOP
{
    public interface IServiceLocator
    {
        T Get<T>();
        void Add<T>(T service);
        void Remove<T>();
    }
}