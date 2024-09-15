namespace GameCOP.Moving
{
    public class CharacterMoveBehaviour : Behaviour, IUpdatable
    {
        private Velocity _velocity;
        private CharacterController _controller;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            _velocity = actor.Get<Velocity>();
            _controller = actor.Get<CharacterController>();
        }

        public void Update(float deltaTime)
        {
            _controller.Value.Move(_velocity.Value * deltaTime);
        }
    }
}