namespace GameCOP.Combat
{
    public class SpellBehaviour : Behaviour, IListener<SpellSetEvent>
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

            Actor.Listen<SpellSetEvent>(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            Actor.Unlisten<SpellSetEvent>(this);
        }

        public void HandleEvent(SpellSetEvent arguments)
        {
            _attacker.Damage = arguments.Value.Damage;
        }
    }
}