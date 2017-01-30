using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylDNDBot
{
    public enum SpellComponent
    {
        Verbal = 1,
        Somatic = 2,
        Material = 4
    }

    public class Spell
    {
        public string Name => name;
        public int Level => level;
        public string School => school;
        public string CastingTime => castingTime;
        public string Range => range;
        public SpellComponent Components => components;
        public string Materials => materials;
        public string Duration => duration;
        public string Description => description;

        private string name;
        private int level;
        private string school;
        private string castingTime;
        private string range;
        private SpellComponent components;
        private string materials;
        private string duration;
        private string description;

        public Spell(string nombre, int lvl, string sch, string cast, string ran, SpellComponent com, string mats, string dur, string desc)
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
    }
}
