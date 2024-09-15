using UnityEngine;

namespace GameCOP.Physics
{
    public class CollisionExtension : ActorExtension
    {
        private int _id;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            _id = gameObject.GetInstanceID();
        }

        public override void Activate()
        {
            base.Activate();

            Actor.SendGlobal
            (
                new CollisionUnitRegister
                {
                    Id = _id,
                    Actor = Actor,
                }
            );
        }

        public override void Deactivate()
        {
            base.Deactivate();

            Actor.SendGlobal(new CollisionUnitUnregister { Id = _id });
        }

        private void OnCollisionEnter(Collision other)
        {
            Actor?.SendGlobal
            (
                new CollisionEnterRequest
                {
                    SourceId = _id,
                    TargetId = other.gameObject.GetInstanceID(),
                }
            );
        }
    }
}