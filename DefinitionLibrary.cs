using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SylDNDBot
{
    [Serializable]
    public class Term
    {
        public string Name {get { return name; } set { name = value; } }
        public string Definition { get { return definition; } set { definition = value; } }

        private string name;
        private string definition;

        public Term(string nombre, string def)
        {
            name = nombre;
            definition = def;
        }

        public override string ToString() { return $"{name}: {definition}"; }
    }

    public static class DefinitionLibrary
    {
        private static List<Term> allDefinitions = new List<Term>();
        private const string LIBRARY_FILE_NAME = "definition_library.dat";

        public static void AddDefinition(string name, string definition)
        {
            if(FindTerm(name) == null)
                allDefinitions.Add(new Term(name, definition));
        }

        public static void RemoveDefinition(string name)
        {
            allDefinitions.RemoveAll(x => x.Name.ToLower() == name.ToLower());
        }

        public static string GetDefinition(string name)
        {
            Term term = FindTerm(name);
            if(term == null)
                return $"Could not find {name} in definition library!";
            return term.ToString();
        }

        public static string EditDefinition(string name, string definition)
        {
            Term term = FindTerm(name);
            if(term == null)
                return $"Could not find {name} in definition library!";
            term.Definition = definition;
            return term.ToString();
        }

        public static void SaveLibrary()
        {
            FileStream stream = new FileStream(LIBRARY_FILE_NAME, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(stream, allDefinitions);
            }
            catch(SerializationException ex)
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
            if(!File.Exists(LIBRARY_FILE_NAME))
            {
                allDefinitions = new List<Term>();
                return;
            }

            FileStream stream = new FileStream(LIBRARY_FILE_NAME, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                allDefinitions = (List<Term>)formatter.Deserialize(stream);
            }
            catch(SerializationException ex)
            {
                Console.WriteLine($"Failed to deserialize: {ex.Message}");
            }
            finally
            {
                stream.Close();
            }
        }

        public static string SearchTerms(string query)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Terms");
            foreach(Term term in allDefinitions.Where(x => x.Name.ToLower().Contains(query.ToLower())).ToArray())
                builder.AppendLine(term.Name);
            return builder.ToString();
        }

        private static Term FindTerm(string name)
        {
            return allDefinitions.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }
    }
}
