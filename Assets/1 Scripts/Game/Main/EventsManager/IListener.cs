namespace GameCOP
{
    public interface IListener
    {
    }

    public interface IListener<T> : IListener where T : struct
    {
        void HandleEvent(T arguments);
    }
}