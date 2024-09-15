using GameCOP.Physics;

namespace GameCOP.Combat
{
    public class MonsterBehaviour : Behaviour, IListener<CollisionEnter>
    {
        private Attacker _attacker;
        // private Character _character;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            _attacker = actor.Get<Attacker>();
        }

        public override void Activate()
        {
            base.Activate();

            Actor.Listen<CollisionEnter>(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            Actor.Unlisten<CollisionEnter>(this);
        }

        public void HandleEvent(CollisionEnter arguments)
        {
            var target = arguments.Target;

            if (target == null) return;
            if (target.Has<MonsterTag>()) return;

            target.Send(new GetDamage { Damage = _attacker.Damage });
        }
    }
}