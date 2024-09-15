using GameCOP.Moving;
using UnityEngine;

namespace GameCOP.Combat
{
    public class SpellWrapper : ActorExtension
    {
        [SerializeField] private Speed speed;
        [SerializeField] private float lifetime = 6f;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            actor.Add<Attacker>();
            actor.Add(speed);
            actor.Add<Lifetime>().Value = lifetime;

            actor.AddBehaviour<SpellBehaviour>();
            actor.AddBehaviour<MoveForwardBehaviour>();
            actor.AddBehaviour<LifetimeBehaviour>();
            actor.AddBehaviour<DealDamageOnTriggerBehaviour>();
            actor.AddBehaviour<DieOnTriggerEnterRequestBehaviour>();
            actor.AddBehaviour<DieBehaviour>();
        }
    }
}