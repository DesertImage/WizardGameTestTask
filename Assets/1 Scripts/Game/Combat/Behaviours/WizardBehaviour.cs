using GameCOP.Spawning;

namespace GameCOP.Combat
{
    public class WizardBehaviour : Behaviour, IAwakable, IListener<PreviousSpellEvent>, IListener<NextSpellEvent>,
        IListener<CastSpellEvent>
    {
        private Spells _spells;
        private ISpawnManager _spawnManager;
        private View _view;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            _spells = actor.Get<Spells>();
            _view = actor.Get<View>();
        }

        public override void Activate()
        {
            base.Activate();

            _spells.CurrentSpell = _spells.Values[_spells.CurrentSpellIndex];

            Actor.Listen<PreviousSpellEvent>(this);
            Actor.Listen<NextSpellEvent>(this);
            Actor.Listen<CastSpellEvent>(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            Actor.Unlisten<PreviousSpellEvent>(this);
            Actor.Unlisten<NextSpellEvent>(this);
            Actor.Unlisten<CastSpellEvent>(this);
        }

        public void OnAwake(IServiceLocator services) => _spawnManager = services.Get<SpawnManager>();

        public void HandleEvent(PreviousSpellEvent arguments)
        {
            var currentIndex = _spells.CurrentSpellIndex;

            currentIndex--;

            if (currentIndex < 0)
            {
                currentIndex = _spells.Values.Count - 1;
            }

            _spells.CurrentSpell = _spells.Values[currentIndex];
            _spells.CurrentSpellIndex = currentIndex;
        }

        public void HandleEvent(NextSpellEvent arguments)
        {
            var currentIndex = _spells.CurrentSpellIndex;

            currentIndex++;

            if (currentIndex >= _spells.Values.Count)
            {
                currentIndex = 0;
            }

            _spells.CurrentSpell = _spells.Values[currentIndex];
            _spells.CurrentSpellIndex = currentIndex;
        }

        public void HandleEvent(CastSpellEvent arguments)
        {
            var spellView = _spawnManager.Spawn<ActorView>(_spells.CurrentSpell.PrefabId);

            var viewTransform = _view.Value.transform;
            var spellViewTransform = spellView.transform;

            spellViewTransform.position = viewTransform.position;
            spellViewTransform.rotation = viewTransform.rotation;

            spellView.Actor.Send(new SpellSetEvent { Value = _spells.CurrentSpell });
        }
    }
}