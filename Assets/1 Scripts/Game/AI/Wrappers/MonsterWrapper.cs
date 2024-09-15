using GameCOP.Combat;
using GameCOP.Moving;
using UnityEngine;

namespace GameCOP.AI
{
    public class MonsterWrapper : ActorExtension
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] [Range(0f, 100f)] private float health = 100f;
        [SerializeField] private Defense defense;
        [SerializeField] private Attacker attacker;
        [SerializeField] private Speed speed;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            actor.Add<NavigationAgent>();
            actor.Add<AITarget>();
            actor.Add<MonsterTag>();
            actor.Add<Velocity>();
            actor.Add<Health>().Value = health;
            actor.Add(characterController);
            actor.Add(defense);
            actor.Add(attacker);
            actor.Add(speed);

            actor.AddBehaviour<HealthBehaviour>();
            actor.AddBehaviour<AITargetBehaviour>();
            actor.AddBehaviour<NavigationAgentBehaviour>();
            actor.AddBehaviour<AIVelocityBehaviour>();
            actor.AddBehaviour<CharacterMoveBehaviour>();
            actor.AddBehaviour<MonsterBehaviour>();
            actor.AddBehaviour<DieBehaviour>();
        }
    }
}