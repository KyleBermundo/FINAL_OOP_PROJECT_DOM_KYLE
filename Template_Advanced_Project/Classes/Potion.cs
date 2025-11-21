using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Template_Advanced_Project.Classes
{
    public class Potion : Item
    {
        public PotionType Type { get; set; }

        public Potion(string name, string id, PotionType type) : base(name, id)
        {
            Type = type;
        }

        public Potion Clone()
        {
            return new Potion(Name, Id, Type);
        }



    }
}
