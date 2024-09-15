using System.Collections.Generic;

namespace GameCOP
{
    public class UpdateManager
    {
        private List<IUpdatable> _updatables = new List<IUpdatable>();

        public void Add(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }

        public void Remove(IUpdatable updatable)
        {
            _updatables.Remove(updatable);
        }

        public void Update(float deltaTime)
        {
            for (var i = 0; i < _updatables.Count; i++)
            {
                _updatables[i].Update(deltaTime);
            }
        }
    }
}