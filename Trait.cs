using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylDNDBot
{
    [Serializable]
    public class Trait
    {
        public string Name { get { return name; } set { name = value; } }
        public string Description { get { return description; } set { description = value; } }

        private string name;
        private string description;

        public Trait(string nombre, string desc)
        {
            name = nombre;
            description = desc;
        }
    }
}
