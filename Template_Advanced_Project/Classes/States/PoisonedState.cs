using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_Advanced_Project.Classes.States
{
    public class PoisonedState : IState
    {
        private static PoisonedState instance = new PoisonedState();
        private PoisonedState() { }

        public static PoisonedState GetInstance()
        {
            return instance;
        }
        public void ExtraDamage(Pokemon pokemon)
        {
            int dmg = pokemon.Hp_MAX / 8;
            pokemon.Hp -= dmg;
        }

        public int GetThreshold()
        {
            return 12;

        }

        public override string ToString()
        {
            return "POISONED";
        }
    }
}
