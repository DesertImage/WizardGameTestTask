using GameCOP.Spawning;
using UnityEngine;

namespace GameCOP
{
    public abstract class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ScriptableObject[] modules;

        protected Core Core;

        private void Awake()
        {
            Core = new Core();

            InitData();
            InitModules();
            InitServices();

            Core.OnAwake(default);
        }

        private void Start() => Core.OnStart();

        protected abstract void InitData();

        protected virtual void InitServices()
        {
            Core.Add<SpawnManager>();
            Core.Add<ActorsManager>();
        }

        protected virtual void InitModules()
        {
            for (var i = 0; i < modules.Length; i++)
            {
                Core.Add(modules[i]);
            }
        }

        private void Update() => Core.Update(Time.deltaTime);
    }
}