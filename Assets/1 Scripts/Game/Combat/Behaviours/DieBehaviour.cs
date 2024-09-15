namespace GameCOP.Combat
{
    public class DieBehaviour : Behaviour, IListener<DieEvent>
    {
        public override void Activate()
        {
            base.Activate();

            Actor.Listen<DieEvent>(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            Actor.Unlisten<DieEvent>(this);
        }

        public void HandleEvent(DieEvent arguments)
        {
            Actor.Release();
        }
    }
}