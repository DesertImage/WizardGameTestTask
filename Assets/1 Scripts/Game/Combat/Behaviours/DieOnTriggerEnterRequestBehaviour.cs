using GameCOP.Physics;

namespace GameCOP.Combat
{
    public class DieOnTriggerEnterRequestBehaviour : Behaviour, IListener<TriggerEnter>
    {
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
            Actor.Send<DieEvent>();
        }
    }
}