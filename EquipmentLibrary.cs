using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SylDNDBot
{
    public static class EquipmentLibrary
    {
        private static List<Equipment> allEquipment = new List<Equipment>();
        private const string LIBRARY_FILE_NAME = "equipment_library.dat";

        public static void AddEquipment(Equipment newEquipment)
        {
            if(FindEquipment(newEquipment.Name) == null)
                allEquipment.Add(newEquipment);
        }

        public static void RemoveEquipment(string name)
        {
            allEquipment.RemoveAll(x => x.Name.ToLower() == name.ToLower());
        }

        public static string GetEquipmentInfo(string name, string field)
        {
            Equipment equipment = FindEquipment(name);
            if(equipment == null)
                return $"Cound not find {name} in equipment library!";
            if(string.IsNullOrEmpty(field))
                field = "All";
            field = char.ToUpper(field[0]) + field.Substring(1).ToLower();

            try
            {
                PropertyInfo equipment_property = equipment.GetType().GetProperty(field);
                return $"{name} {field}\n{equipment_property.GetValue(equipment)}";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }

        public static string EditEquipmentInfo(string name, string field, string newValue)
        {
            Equipment equipment = FindEquipment(name);
            if(equipment == null)
                return $"Could not find {name} in equipment library!";
            if(string.IsNullOrEmpty(field))
                return "User did not provide a field to edit";

            field = char.ToUpper(field[0]) + field.Substring(1).ToLower();

            try
            {
                PropertyInfo equipment_property = equipment.GetType().GetProperty(field);
                equipment_property.SetValue(equipment, newValue);
                return $"{name} {field}\n{equipment_property.GetValue(equipment)}";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }

        public static void SaveLibrary()
        {
            FileStream stream = new FileStream(LIBRARY_FILE_NAME, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(stream, allEquipment);
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
                allEquipment = new List<Equipment>();
                return;
            }

            FileStream stream = new FileStream(LIBRARY_FILE_NAME, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                allEquipment = (List<Equipment>)formatter.Deserialize(stream);
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

        public static string SearchEquipment(string query)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Equipment");
            foreach(Equipment equip in allEquipment.Where(x => x.Name.ToLower().Contains(query.ToLower())).ToArray())
                builder.AppendLine(equip.Name);
            return builder.ToString();
        }

        public static Equipment FindEquipment(string name)
        {
            return allEquipment.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }
    }
}
