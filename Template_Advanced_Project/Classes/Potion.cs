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
        private PotionType type;

        public PotionType Type
        {
            get { return type; } 
            set { type = value; }
        }

        public Potion(string name, int price, PotionType type) : base(name, price)
        {
            this.type = type;
        }

        public Potion Clone()
        {
            return new Potion(this.Name, this.Price, this.type);
        }


    }
}
