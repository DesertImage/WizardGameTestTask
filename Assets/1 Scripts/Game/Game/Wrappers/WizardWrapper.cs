using System.Collections.Generic;
using GameCOP.Combat;
using GameCOP.Input;
using GameCOP.Moving;
using UnityEngine;

namespace GameCOP
{
    public class WizardWrapper : ActorExtension
    {
        [SerializeField] private List<ScriptableSpell> spells;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private MoveBounds moveBounds;
        [SerializeField] private Speed speed;
        [SerializeField] private Health health;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            var spellsData = actor.Add<Spells>();

            if (spellsData.Values != null)
            {
                spellsData.Values.AddRange(spells);
            }
            else
            {
                spellsData.Values = new List<ISpell>(spells);
            }

            actor.Add<AxisInput>();
            actor.Add<Velocity>();
            actor.Add(health);
            actor.Add<Defense>().Value = .5f;
            actor.Add(speed);
            actor.Add(moveBounds);
            actor.Add(characterController);

            actor.AddBehaviour<AxisInputBehaviour>();
            actor.AddBehaviour<PlayerInputBehaviour>();
            actor.AddBehaviour<HealthBehaviour>();
            actor.AddBehaviour<WizardBehaviour>();
            actor.AddBehaviour<LookAtMoveDirectionBehaviour>();
            actor.AddBehaviour<InputToVelocityBehaviour>();
            actor.AddBehaviour<CharacterMoveBehaviour>();
            actor.AddBehaviour<MoveBoundsBehaviour>();
            actor.AddBehaviour<DieBehaviour>();
        }
    }
}