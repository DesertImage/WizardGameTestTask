using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCOP.Spawning
{
    [Serializable]
    public struct SpawningNode
    {
        public int Id;
        public MonoPoolable Instance;
        public int PreRegisterCount;
    }

    [CreateAssetMenu(fileName = "ObjectsLibrary", menuName = "Game/Libraries/ObjectsLibrary")]
    public class ScriptableObjectsLibrary : ScriptableObject, IAwakable, IInitOnly
    {
        [SerializeField] private List<SpawningNode> spawningNodes;

        public void OnAwake(IServiceLocator services)
        {
            var spawnManager = services.Get<SpawnManager>();
            
            for (var i = 0; i < spawningNodes.Count; i++)
            {
                var node = spawningNodes[i];

                spawnManager.Register(node.Id, node.Instance, node.PreRegisterCount);
            }
        }
    }
}