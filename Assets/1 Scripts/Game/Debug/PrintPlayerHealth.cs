using GameCOP.Combat;

namespace GameCOP
{
    public class PrintPlayerHealth : IAwakable, IDestroyable, IListener<GetDamage>, IListener<DieEvent>
    {
        private IActor _player;
        private Health _health;

        public void OnAwake(IServiceLocator services)
        {
            _player = services.Get<PlayerData>().Value;
            _health = _player.Get<Health>();

            _player.Listen<GetDamage>(this);
            _player.Listen<DieEvent>(this);
        }

        public void OnDestroy()
        {
            _player.Unlisten<GetDamage>(this);
            _player.Unlisten<DieEvent>(this);
        }

        public void HandleEvent(GetDamage arguments)
        {
            UnityEngine.Debug.Log($"Player get damage: {arguments.Damage}. Health: {_health.Value}");
        }

        public void HandleEvent(DieEvent arguments)
        {
            UnityEngine.Debug.Log($"<color=red>Player is DEAD</color>");
        }
    }
}