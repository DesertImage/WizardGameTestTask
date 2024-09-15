using GameCOP.Input;
using UnityEngine;

namespace GameCOP.Moving
{
    public class LookAtMoveDirectionBehaviour : Behaviour, IUpdatable
    {
        private AxisInput _input;
        private View _view;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            _input = actor.Get<AxisInput>();
            _view = actor.Get<View>();
        }

        public void Update(float deltaTime)
        {
            var input = _input.Axis;

            if (input == Vector2.zero) return;

            var transform = _view.Value.transform;

            transform.rotation = Quaternion.LookRotation
            (
                new Vector3(input.x, 0f, input.y),
                Vector3.up
            );
        }
    }
}