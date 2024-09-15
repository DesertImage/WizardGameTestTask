using GameCOP.Game;
using GameCOP.Physics;
using UnityEngine;

namespace GameCOP
{
    public class WizardGame : EntryPoint
    {
        [SerializeField] private MonstersSpawn monstersSpawn;

        protected override void InitData()
        {
            Core.Add(monstersSpawn);
        }

        protected override void InitServices()
        {
            base.InitServices();

            Core.Add<PhysicsRouter>();

            Core.Add<SpawnPlayer>();

            Core.Add<MonstersSpawnManager>();
#if DEBUG
            Core.Add<PrintPlayerHealth>();
#endif
        }
    }
}