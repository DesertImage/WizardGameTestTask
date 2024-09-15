using GameCOP.AI;
using GameCOP.Combat;
using GameCOP.Spawning;
using UnityEngine;
using CharacterController = GameCOP.Moving.CharacterController;

namespace GameCOP
{
    public class MonstersSpawnManager : IAwakable, IDestroyable, IUpdatable, IListener<DieEvent>
    {
        private enum SpawnDirection : byte
        {
            Left,
            Right,
            Top,
            Bottom
        }

        private MonstersSpawn _monstersSpawn;
        private ISpawnManager _spawnManager;
        private UpdateManager _updateManager;
        private IEventsManager _eventsManager;

        private PlayerData _playerData;

        private Camera _camera;

        public void OnAwake(IServiceLocator services)
        {
            _monstersSpawn = services.Get<MonstersSpawn>();
            _spawnManager = services.Get<SpawnManager>();
            _updateManager = services.Get<UpdateManager>();
            _eventsManager = services.Get<EventsManager>();

            _playerData = services.Get<PlayerData>();

            _eventsManager.Listen<DieEvent>(this);

            _camera = Camera.main;
        }

        public void OnDestroy()
        {
            _eventsManager.Unlisten<DieEvent>(this);
        }

        private void Spawn()
        {
            _monstersSpawn.Count++;

            var monsterId = Random.Range
            (
                (int)ObjectId.Enemy1,
                (int)(ObjectId.Enemy3 + 1)
            );

            var monsterView = _spawnManager.Spawn<ActorView>
            (
                monsterId,
                GetRandomOffscreenPosition(_camera)
            );

            monsterView.Actor.Send(new AITargetSetEvent { Value = _playerData.Value });
        }

        private static Vector3 GetRandomOffscreenPosition(Camera camera)
        {
            const float monsterSizeOffset = 1f;

            var cameraSize = camera.orthographicSize * 2f;
            var cameraPosition = camera.transform.position;

            var leftBorder = cameraPosition + Vector3.left * cameraSize;
            var rightBorder = cameraPosition + Vector3.right * cameraSize;
            var topBorder = cameraPosition + Vector3.forward * cameraSize;
            var bottomBorder = cameraPosition + Vector3.back * cameraSize;

            var direction = (SpawnDirection)Random.Range(0, 4);

            var position = Vector3.zero;

            switch (direction)
            {
                case SpawnDirection.Left:
                    position.x = leftBorder.x - monsterSizeOffset;
                    position.z = Random.Range(bottomBorder.z, topBorder.z);
                    break;

                case SpawnDirection.Right:
                    position.x = rightBorder.x + monsterSizeOffset;
                    position.z = Random.Range(bottomBorder.z, topBorder.z);
                    break;

                case SpawnDirection.Top:
                    position.x = Random.Range(leftBorder.x, rightBorder.x);
                    position.z = topBorder.z + monsterSizeOffset;
                    break;

                case SpawnDirection.Bottom:
                    position.x = Random.Range(leftBorder.x, rightBorder.x);
                    position.z = bottomBorder.z - monsterSizeOffset;
                    break;
            }


            return position;
        }

        public void Update(float deltaTime)
        {
            _monstersSpawn.SpawnTimer -= deltaTime;

            if (_monstersSpawn.SpawnTimer > 0f) return;

            if (_monstersSpawn.Count < _monstersSpawn.MaxCount)
            {
                _monstersSpawn.SpawnTimer = Random.Range(2f, 8f);

                Spawn();
            }
            else
            {
                _updateManager.Remove(this);
            }
        }

        public void HandleEvent(DieEvent arguments)
        {
            if (!arguments.Value.Has<MonsterTag>()) return;

            _monstersSpawn.Count--;
            Spawn();
        }
    }
}