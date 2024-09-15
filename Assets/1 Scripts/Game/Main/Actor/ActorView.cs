using UnityEngine;

namespace GameCOP
{
    public class ActorView : MonoPoolable
    {
        public IActor Actor { get; private set; }

        [SerializeField] private ActorExtension[] actorExtensions;

        public void SetActor(IActor actor)
        {
            Actor = actor;

            if (!actor.Has<View>())
            {
                actor.Add<View>();
            }

            actor.Get<View>().Value = this;

            for (var i = 0; i < actorExtensions.Length; i++)
            {
                actorExtensions[i].Bind(actor);
            }

            Actor.ReleaseRequest += ActorOnReleaseRequest;
        }

        private void ActivateExtensions()
        {
            for (var i = 0; i < actorExtensions.Length; i++)
            {
                actorExtensions[i].Activate();
            }
        }

        private void DeactivateExtensions()
        {
            for (var i = 0; i < actorExtensions.Length; i++)
            {
                actorExtensions[i].Deactivate();
            }
        }

        public override void OnSpawn() => ActivateExtensions();

        public override void OnRelease()
        {
            DeactivateExtensions();

            if (Actor == null) return;

            Actor.ReleaseRequest -= ActorOnReleaseRequest;

            Actor.Release();
            Actor = null;
        }

        private void ActorOnReleaseRequest(IPoolable obj)
        {
            obj.ReleaseRequest -= ActorOnReleaseRequest;
            Actor = null;

            Release();
        }

        private void OnValidate()
        {
            if (actorExtensions is { Length: 0 })
            {
                actorExtensions = GetComponentsInChildren<ActorExtension>();
            }
        }
    }
}