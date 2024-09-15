namespace GameCOP.Spawning
{
    public static class SpawningExtensions
    {
        public static T Spawn<T>(this ISpawnManager manager, ObjectId id) where T : MonoPoolable
        {
            return manager.Spawn<T>((int)id);
        }
    }
}