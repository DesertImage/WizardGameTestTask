using GameCOP.Moving;

namespace GameCOP.AI
{
    public class AIVelocityBehaviour : Behaviour, IUpdatable
    {
        private NavigationAgent _navigationAgent;
        private Velocity _velocity;
        private View _view;
        private Speed _speed;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            _navigationAgent = actor.Get<NavigationAgent>();
            _velocity = actor.Get<Velocity>();
            _view = actor.Get<View>();
            _speed = actor.Get<Speed>();
        }

        public void Update(float deltaTime)
        {
            var position = _view.Value.transform.position;
            var targetPosition = _navigationAgent.CurrentPoint;

            var direction = targetPosition - position;

            _velocity.Value = direction.normalized * _speed.Value;
        }
    }
}