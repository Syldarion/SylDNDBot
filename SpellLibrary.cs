using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SylDNDBot
{
    public static class SpellLibrary
    {
        public static List<Spell> AllSpells => allSpells;

        private static List<Spell> allSpells = new List<Spell>();

        public static void AddSpell(Spell newSpell)
        {
            if (FindSpell(newSpell.Name) == null)
                allSpells.Add(newSpell);
        }

        public static string GetSpellInfo(string name, string field)
        {
            Spell spell = FindSpell(name);
            if (spell == null)
                return $"Could not find {name} in spell library!";

            PropertyInfo spell_property = spell.GetType().GetProperty(field);
            //FieldInfo spell_field = spell.GetType().GetField(field);

            return $"{name} {field}: {spell_property.GetValue(spell)}";
        }

        public static string GetSpellLevelSchool(string name)
        {
            Spell spell = FindSpell(name);
            if (spell == null)
                return $"Could not find {name} in spell library!";

            return $"{name}: {spell.School} {spell.Level}";
        }

        private static Spell FindSpell(string name)
        {
            return allSpells.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }

        private static Spell[] SearchSpells(string name)
        {
            return allSpells.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToArray();
        }
    }
}
