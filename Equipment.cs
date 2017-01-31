using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylDNDBot
{
    public class Equipment
    {
        public string Name { get { return name; } set { name = value; } }
        public string Cost { get { return cost; } set { cost = value; } }
        public string Weight { get { return weight; } set { weight = value; } }
        public string Description { get { return description; } set { description = value; } }
        public string All => AllInfo();

        private string name;
        private string cost;
        private string weight;
        private string description;

        public Equipment(string nombre, string co, string we, string desc)
        {
            name = nombre;
            cost = co;
            weight = we;
            description = desc;
        }

        public string AllInfo()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Name: {name}");
            builder.AppendLine($"Cost: {cost}");
            builder.AppendLine($"Weight: {weight}");
            builder.AppendLine($"Description: {description}");

            return builder.ToString();
        }
    }
}
