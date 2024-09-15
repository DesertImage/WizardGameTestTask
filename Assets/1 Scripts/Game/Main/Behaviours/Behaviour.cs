namespace GameCOP
{
    public abstract class Behaviour : Poolable, IBehaviour
    {
        protected IActor Actor { get; private set; }
        protected IEventsManager GlobalEventsManager { get; private set; }

        public void Inject(IEventsManager manager) => GlobalEventsManager = manager;

        protected bool IsActive { get; private set; }

        public virtual void Activate() => IsActive = true;
        public virtual void Deactivate() => IsActive = false;

        public virtual void Bind(IActor actor) => Actor = actor;

        public override void OnSpawn()
        {
        }

        public override void OnRelease()
        {
            if (!IsActive) return;
            Deactivate();
        }
    }
}