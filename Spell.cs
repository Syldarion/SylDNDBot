using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SylDNDBot
{
    [Serializable]
    public class Spell
    {
        public string Name {get { return name; } set { name = value; } }
        public int Level {get { return level; } set { level = value; } }
        public string School { get { return school; } set { school = value; } }
        public string CastingTime { get { return castingTime; } set { castingTime = value; } }
        public string Range { get { return range; } set { range = value; } }
        public string Components { get { return components; } set { components = value; } }
        public string Materials { get { return materials; } set { materials = value; } }
        public string Duration { get { return duration; } set { duration = value; } }
        public string Description { get { return description; } set { description = value; } }
        public string All => AllInfo();

        private string name;
        private int level;
        private string school;
        private string castingTime;
        private string range;
        private string components;
        private string materials;
        private string duration;
        private string description;

        public Spell(string nombre, int lvl, string sch, string cast, string ran, string com, string mats, string dur, string desc)
        {
            name = nombre;
            level = lvl;
            school = sch;
            castingTime = cast;
            range = ran;
            components = com;
            materials = mats;
            duration = dur;
            description = desc;
        }

        public string AllInfo()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Level: {level}");
            builder.AppendLine($"School: {school}");
            builder.AppendLine($"Cast Time: {castingTime}");
            builder.AppendLine($"Range: {range}");
            builder.AppendLine($"Components: {components}");
            builder.AppendLine($"Duration: {duration}");
            builder.AppendLine($"Description: {description}");

            return builder.ToString();
        }
    }
}
