using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_Advanced_Project.Classes
{
    public class Item
    {

        private string name;
        private int price;

        public string Name { get => name; set => name = value; }
        public int Price { get => price; set => price = value; }


        public Item(string name, int price)
        {
            this.Name = name;
            this.Price = price;

        }

        public override string ToString()
        {
            return $"{Name} - {Price} GP";
        } 
      
    }
}
