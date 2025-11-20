using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_Advanced_Project.Classes
{
    public class Item
    {

        private string Name;
        private string Id;

        public string Name1 { get => Name; set => Name = value; }
        public string Id1 { get => Id; set => Id = value; }


        public Item(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public override string ToString()
        {
            return Name + Id;
            

        } 
      
    }
}
