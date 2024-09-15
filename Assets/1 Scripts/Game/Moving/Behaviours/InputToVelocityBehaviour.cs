using GameCOP.Input;
using UnityEngine;

namespace GameCOP.Moving
{
    public class InputToVelocityBehaviour : Behaviour, IUpdatable
    {
        private AxisInput _input;
        private Velocity _velocity;
        private Speed _speed;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            _input = actor.Get<AxisInput>();
            _velocity = actor.Get<Velocity>();
            _speed = actor.Get<Speed>();
        }

        public void Update(float deltaTime)
        {
            var input = _input.Axis;
            _velocity.Value = new Vector3(input.x, 0f, input.y) * _speed.Value;
        }
    }
}