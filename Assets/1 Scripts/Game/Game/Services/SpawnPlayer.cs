using GameCOP.Spawning;

namespace GameCOP
{
    public class SpawnPlayer : IAwakable
    {
        public void OnAwake(IServiceLocator services)
        {
            var spawnManager = services.Get<SpawnManager>();

            var instance = spawnManager.Spawn<ActorView>(ObjectId.Mage);

            services.Add(new PlayerData { Value = instance.Actor });
        }
    }
}