using GameCOP.Physics;

namespace GameCOP.Combat
{
    public class DealDamageOnTriggerBehaviour : Behaviour, IListener<TriggerEnter>
    {
        private Attacker _attacker;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            _attacker = actor.Get<Attacker>();
        }

        public override void Activate()
        {
            base.Activate();

            Actor.Listen<TriggerEnter>(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            Actor.Unlisten<TriggerEnter>(this);
        }

        public void HandleEvent(TriggerEnter arguments)
        {
            arguments.Target?.Send(new GetDamage { Damage = _attacker.Damage });
        }
    }
}