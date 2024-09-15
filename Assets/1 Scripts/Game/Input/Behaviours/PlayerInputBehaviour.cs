using GameCOP.Combat;
using UnityEngine;

namespace GameCOP.Input
{
    public class PlayerInputBehaviour : Behaviour, IUpdatable
    {
        public void Update(float deltaTime)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
            {
                Actor.Send<PreviousSpellEvent>();
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.W))
            {
                Actor.Send<NextSpellEvent>();
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.X))
            {
                Actor.Send<CastSpellEvent>();
            }
        }
    }
}