using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_Advanced_Project.Classes.States
{
    public class ParalyzedState : IState
    {
        private static ParalyzedState instance = new ParalyzedState();
        private ParalyzedState() { }
        internal static ParalyzedState GetInstance()
        {
            return instance;
        }

        public void ExtraDamage(Pokemon pokemon)
        {
           //this state shouldnt do any damage since it paralyses only 
        }

        public int GetThreshold()
        {
            return 12;
        }


        public override string ToString()
        {
            return "PARALYZED";
        }
    }
}
