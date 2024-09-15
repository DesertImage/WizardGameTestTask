namespace GameCOP.Combat
{
    public class HealthBehaviour : Behaviour, IListener<GetDamage>
    {
        private Health _health;
        private Defense _defense;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            _health = actor.Get<Health>();
            _defense = actor.Get<Defense>();
        }

        public override void Activate()
        {
            base.Activate();

            Actor.Listen<GetDamage>(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            Actor.Unlisten<GetDamage>(this);
        }

        public void HandleEvent(GetDamage arguments)
        {
            var damage = arguments.Damage;

            if (_defense != null)
            {
                damage *= _defense.Value;
            }

            _health.Value -= damage;

            if (_health.Value > 0) return;

            var dieEvent = new DieEvent { Value = Actor };

            Actor.SendGlobal(dieEvent);
            Actor.Send(dieEvent);
        }
    }
}