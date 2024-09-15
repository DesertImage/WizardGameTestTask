using UnityEngine.AI;

namespace GameCOP.AI
{
    public class NavigationAgentBehaviour : Behaviour, IUpdatable
    {
        private NavigationAgent _navigationAgent;
        private AITarget _target;
        private View _view;

        public override void Bind(IActor actor)
        {
            base.Bind(actor);

            _navigationAgent = actor.Get<NavigationAgent>();
            _target = actor.Get<AITarget>();
            _view = actor.Get<View>();
        }

        public void Update(float deltaTime)
        {
            RecalculatePath(_target.Value);
        }

        private void RecalculatePath(IActor target)
        {
            if (target == null) return;
            if (!_target.Value.Has<View>()) return;

            var position = _view.Value.transform.position;
            var targetPosition = _target.Value.Get<View>().Value.transform.position;

            var navMeshPath = _navigationAgent.Path;

            NavMesh.CalculatePath(position, targetPosition, NavMesh.AllAreas, navMeshPath);

            if (navMeshPath.status == NavMeshPathStatus.PathInvalid) return;

            var points = _navigationAgent.Points;

            points.Clear();

            for (var i = 0; i < navMeshPath.corners.Length; i++)
            {
                var point = navMeshPath.corners[i];
                points.Enqueue(point);
            }

            points.Dequeue();

            if (points.Count == 0) return;
            
            _navigationAgent.CurrentPoint = points.Dequeue();
        }
    }
}