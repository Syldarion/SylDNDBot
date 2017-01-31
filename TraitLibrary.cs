using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SylDNDBot
{
    public static class TraitLibrary
    {
        private static List<Trait> allTraits = new List<Trait>();
        private const string LIBRARY_FILE_NAME = "trait_library.dat";

        public static void AddTrait(Trait newTrait)
        {
            if(FindTrait(newTrait.Name) == null)
                allTraits.Add(newTrait);
        }

        public static void RemoveTrait(string name)
        {
            allTraits.RemoveAll(x => x.Name.ToLower() == name.ToLower());
        }

        public static string GetTraitInfo(string name)
        {
            Trait trait = FindTrait(name);
            if(trait == null)
                return $"Cound not find {name} in trait library!";
            return $"{trait.Name}\n{trait.Description}";
        }

        public static string EditTraitInfo(string name, string value)
        {
            Trait trait = FindTrait(name);
            if(trait == null)
                return $"Could not find {name} in trait library!";
            trait.Description = value;
            return $"{trait.Name}\n{trait.Description}";
        }

        public static void SaveLibrary()
        {
            FileStream stream = new FileStream(LIBRARY_FILE_NAME, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(stream, allTraits);
            }
            catch (SerializationException ex)
            {
                Console.WriteLine($"Failed to serialize: {ex.Message}");
            }
            finally
            {
                stream.Close();
            }
        }

        public static void LoadLibrary()
        {
            if (!File.Exists(LIBRARY_FILE_NAME))
            {
                allTraits = new List<Trait>();
                return;
            }

            FileStream stream = new FileStream(LIBRARY_FILE_NAME, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                allTraits = (List<Trait>)formatter.Deserialize(stream);
            }
            catch (SerializationException ex)
            {
                Console.WriteLine($"Failed to deserialize: {ex.Message}");
            }
            finally
            {
                stream.Close();
            }
        }

        public static string SearchTraits(string query)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Spells");
            foreach (Trait trait in allTraits.Where(x => x.Name.ToLower().Contains(query.ToLower())).ToArray())
                builder.AppendLine(trait.Name);
            return builder.ToString();
        }

        private static Trait FindTrait(string name)
        {
            return allTraits.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }
    }
}