using GameCOP.Spawning;

namespace GameCOP
{
    public class ActorsManager : IAwakable
    {
        private readonly IPool<IActor> _pool = new Pool<IActor>(10, () => new Actor());
        private readonly DataPool _dataPool = new DataPool();

        private IServiceLocator _services;
        private UpdateManager _updateManager;

        public void OnAwake(IServiceLocator services)
        {
            _services = services;

            _services.Get<SpawnManager>().OnPreSpawn += OnPreSpawn;
            _updateManager = services.Get<UpdateManager>();
        }

        private IActor GetNewActor()
        {
            var actor = _pool.GetNewInstance(true);
            actor.Inject(_services);

            _updateManager.Add(actor);

            actor.ReleaseRequest += ActorOnReleaseRequest;
            return actor;
        }

        private void ActorOnReleaseRequest(IPoolable obj)
        {
            obj.ReleaseRequest -= ActorOnReleaseRequest;

            var actor = (IActor)obj;
            _updateManager.Remove(actor);
        }

        public IData GetDataInstance<T>() where T : IData, new() => _dataPool.GetNewInstance<T>();

        public IBehaviour GetBehaviourInstance<T>() where T : IBehaviour, new() => _dataPool.GetNewInstance<T>();

        private void OnPreSpawn(IPoolable obj)
        {
            if (obj is not ActorView actorView) return;
            actorView.SetActor(GetNewActor());
        }
    }
}