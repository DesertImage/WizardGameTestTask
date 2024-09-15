using System.Collections.Generic;

namespace GameCOP.Combat
{
    public class Spells : Data
    {
        public List<ISpell> Values = new List<ISpell>();
        public int CurrentSpellIndex;

        public ISpell CurrentSpell;

        public override void OnRelease()
        {
            base.OnRelease();

            Values.Clear();
            CurrentSpell = null;
            CurrentSpellIndex = 0;
        }
    }
}