namespace GameCOP
{
    public interface IBehaviour : IPoolable
    {
        void Activate();
        void Deactivate();
        
        void Bind(IActor actor);
        
        void Inject(IEventsManager manager);
    }
}