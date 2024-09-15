namespace GameCOP.AI
{
    public class AITargetBehaviour : Behaviour, IListener<AITargetSetEvent>
    {
        private AITarget _aiTarget;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            _aiTarget = actor.Get<AITarget>();
        }

        public override void Activate()
        {
            base.Activate();

            Actor.Listen<AITargetSetEvent>(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            Actor.Unlisten<AITargetSetEvent>(this);
        }

        public void HandleEvent(AITargetSetEvent arguments)
        {
            _aiTarget.Value = arguments.Value;
        }
    }
}