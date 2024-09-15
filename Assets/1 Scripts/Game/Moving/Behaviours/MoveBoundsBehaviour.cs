using UnityEngine;

namespace GameCOP.Moving
{
    public class MoveBoundsBehaviour : Behaviour, IUpdatable
    {
        private MoveBounds _bounds;
        private View _view;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            _bounds = actor.Get<MoveBounds>();
            _view = actor.Get<View>();
        }

        public void Update(float deltaTime)
        {
            var bounds = _bounds.Value;

            var position = _view.Value.transform.position;
            
            _view.Value.transform.position = new Vector3
            (
                Mathf.Clamp(position.x, bounds.min.x, bounds.max.x), 
                0f,
                Mathf.Clamp(position.z, bounds.min.z, bounds.max.z)
            );
        }
    }
}