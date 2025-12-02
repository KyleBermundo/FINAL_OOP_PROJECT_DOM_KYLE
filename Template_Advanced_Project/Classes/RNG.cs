using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_Advanced_Project.Classes
{
    //part of template given
    public class RNG : Random
    {
        private static RNG instance = new RNG();
        private RNG() : base() { }
        public static RNG GetInstance()
        {
            return instance;
        }
    }
}
