using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_Advanced_Project.Classes
{
    // part of template given
    public class Move
    {
        private string name;
        private int accuracy;
        private int pp;    //max value is 56
        private int pp_Max;//max value is 56

        public string Name { get => name.ToUpper(); set => name = value; }
        public int Accuracy { get => accuracy; set => accuracy = value; }
        public int PP { get => pp; set => pp = (value > 56) ? 56 : value; }
        public int PP_Max { get => pp_Max; set => pp_Max = (value > 56) ? 56 : value; }
    
        public Move(string name, int accuracy, int pp_max)
        {
            this.name = name;
            this.accuracy = accuracy;
            this.PP_Max = pp_max;
            this.PP = pp_max;
        }
        
        public Move Clone()
        {
            return new Move(this.name, this.accuracy, this.pp_Max);
        }
        public void Increase_PP_Max(int max)
        {
            this.PP_Max += max;//Max Value is 56 ( the validation is done in the Set=> ) 
        }
        public void Resest_PP()
        {
            this.PP = this.PP_Max;
        }

    }
}
