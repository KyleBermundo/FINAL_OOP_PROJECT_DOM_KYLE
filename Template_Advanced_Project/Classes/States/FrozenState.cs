using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_Advanced_Project.Classes.States
{
    public class FrozenState : IState
    {
        private static FrozenState instance = new FrozenState();
        private FrozenState() { }

        public static FrozenState GetInstance()
        {
            return instance;
        }

        public void ExtraDamage(Pokemon pokemon)
        {
           //frozen is a stun so no damage
        }

        public int GetThreshold()
        {
            return 25;

        }


        public override string ToString()
        {
            return "FROZEN";
        }
    }
}
