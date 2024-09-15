namespace GameCOP.Moving
{
    public class MoveForwardBehaviour : Behaviour, IUpdatable
    {
        private View _view;
        private Speed _speed;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            _view = actor.Get<View>();
            _speed = actor.Get<Speed>();
        }

        public void Update(float deltaTime)
        {
            var transform = _view.Value.transform;
            transform.position += transform.forward * _speed.Value * deltaTime;
        }
    }
}