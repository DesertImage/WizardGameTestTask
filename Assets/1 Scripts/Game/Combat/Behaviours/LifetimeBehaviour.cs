namespace GameCOP.Combat
{
    public class LifetimeBehaviour : Behaviour, IUpdatable
    {
        private Lifetime _lifetime;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            _lifetime = actor.Get<Lifetime>();
        }

        public void Update(float deltaTime)
        {
            _lifetime.Value -= deltaTime;
            if (_lifetime.Value > 0) return;
            Actor.Send<DieEvent>();
        }
    }
}