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
        private string id;

        public string Name { get => Name; set => Name = value; }
        public string Id { get => Id; set => Id = value; }


        public Item(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }

        public override string ToString()
        {
            return Name + Id;
            

        } 
      
    }
}
