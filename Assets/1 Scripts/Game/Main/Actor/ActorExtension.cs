using UnityEngine;

namespace GameCOP
{
    public abstract class ActorExtension : MonoBehaviour, IActorExtension
    {
        protected IActor Actor;

        public virtual void Bind(IActor actor)
        {
            Actor = actor;
        }

        public virtual void Activate()
        {
        }

        public virtual void Deactivate()
        {
        }
    }
}