
namespace GameCOP
{
    public interface IInjectable<T>
    {
        void Inject(T services);
    }
}