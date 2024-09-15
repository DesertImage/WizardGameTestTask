using System.Collections.Generic;

namespace GameCOP.Physics
{
    public class PhysicsRouter : IAwakable, IDestroyable, IListener<CollisionUnitRegister>,
        IListener<CollisionUnitUnregister>,
        IListener<CollisionEnterRequest>, IListener<TriggerEnterRequest>
    {
        private readonly IDictionary<int, IActor> _actors = new Dictionary<int, IActor>();

        private IEventsManager _eventsManager;

        public void OnAwake(IServiceLocator services)
        {
            _eventsManager = services.Get<EventsManager>();

            _eventsManager.Listen<CollisionUnitRegister>(this);
            _eventsManager.Listen<CollisionUnitUnregister>(this);
            _eventsManager.Listen<CollisionEnterRequest>(this);
            _eventsManager.Listen<TriggerEnterRequest>(this);
        }

        public void OnDestroy()
        {
            _eventsManager.Unlisten<CollisionUnitRegister>(this);
            _eventsManager.Unlisten<CollisionUnitUnregister>(this);
            _eventsManager.Unlisten<CollisionEnterRequest>(this);
            _eventsManager.Unlisten<TriggerEnterRequest>(this);
        }

        public void Register(int id, IActor actor) => _actors.TryAdd(id, actor);

        public void Unregister(int id)
        {
            if (!_actors.ContainsKey(id)) return;
            _actors.Remove(id);
        }

        public void CollisionEnterExecute(int sourceId, int targetId)
        {
            if (!_actors.ContainsKey(sourceId)) return;

            _actors.TryGetValue(targetId, out var target);

            _actors[sourceId].Send(new CollisionEnter { Target = target });
        }

        public void TriggerEnterExecute(int sourceId, int targetId)
        {
            if (!_actors.ContainsKey(sourceId)) return;

            _actors.TryGetValue(targetId, out var target);

            _actors[sourceId].Send(new TriggerEnter { Target = target });
        }

        public void HandleEvent(CollisionUnitRegister arguments) => Register(arguments.Id, arguments.Actor);
        public void HandleEvent(CollisionUnitUnregister arguments) => Unregister(arguments.Id);

        public void HandleEvent(CollisionEnterRequest arguments)
        {
            CollisionEnterExecute(arguments.SourceId, arguments.TargetId);
        }

        public void HandleEvent(TriggerEnterRequest arguments)
        {
            TriggerEnterExecute(arguments.SourceId, arguments.TargetId);
        }
    }
}