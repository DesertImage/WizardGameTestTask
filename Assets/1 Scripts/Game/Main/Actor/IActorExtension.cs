namespace GameCOP
{
    public interface IActorExtension
    {
        void Bind(IActor actor);
        
        void Activate();
        void Deactivate();
    }
}