using UnityEngine;

namespace GameCOP.Input
{
    public class AxisInputBehaviour : Behaviour, IUpdatable
    {
        private AxisInput _input;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            _input = actor.Get<AxisInput>();
        }

        public void Update(float deltaTime)
        {
            _input.Axis = new Vector2
            (
                UnityEngine.Input.GetAxis("Horizontal"),
                UnityEngine.Input.GetAxis("Vertical")
            );
        }
    }
}