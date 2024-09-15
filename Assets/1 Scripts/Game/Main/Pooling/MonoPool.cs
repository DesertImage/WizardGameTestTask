using System.Collections.Generic;
using UnityEngine;

namespace GameCOP
{
    public class MonoPool : PrototypePool<MonoPoolable>
    {
        private readonly IDictionary<int, int> _instanceIdToOriginalId = new Dictionary<int, int>();

        private readonly Transform _poolParent = new GameObject("Pool").transform;

        protected override MonoPoolable CreateNew(MonoPoolable original)
        {
            original.gameObject.SetActive(false);
            var instance = Object.Instantiate(original);
            original.gameObject.SetActive(true);

            return instance;
        }

        protected override int GetId(MonoPoolable prototype) => prototype.gameObject.GetInstanceID();

        protected override void OnSpawn(MonoPoolable original, MonoPoolable instance, bool isSilently = false)
        {
            base.OnSpawn(original, instance, isSilently);

            _instanceIdToOriginalId.Add(GetId(instance), GetId(original));

            instance.transform.SetParent(null);

            if (!isSilently)
            {
                instance.gameObject.SetActive(true);
            }
        }

        protected override void OnRelease(MonoPoolable instance, bool isSilently = false)
        {
            base.OnRelease(instance, isSilently);

            _instanceIdToOriginalId.Remove(GetId(instance));

            instance.gameObject.SetActive(false);
            instance.transform.SetParent(_poolParent);
        }

        public override void Release(MonoPoolable instance)
        {
            var instanceId = GetId(instance);
            var originalId = _instanceIdToOriginalId[instanceId];

            if (!Instances.TryGetValue(originalId, out var poolData)) return;

            OnRelease(instance);
            poolData.Instances.Push(instance);
        }
    }
}