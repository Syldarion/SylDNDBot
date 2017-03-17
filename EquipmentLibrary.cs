using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SylDNDBot
{
    public static class EquipmentLibrary
    {
        private const string GET_EQUIPMENT_CMD = "get_equipment";
        private const string ADD_EQUIPMENT_CMD = "add_equipment";
        private const string REMOVE_EQUIPMENT_CMD = "remove_equipment";
        private const string SEARCH_EQUIPMENT_CMD = "search_equipment";

        public static string AddEquipment(string name, string cost, string weight, string description)
        {
            name = name.ToLower();

            string response;

            using (MySqlConnection conn = new MySqlConnection(ServerInfo.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand add_equipment_cmd = new MySqlCommand())
                {
                    add_equipment_cmd.Connection = conn;
                    add_equipment_cmd.CommandText = ADD_EQUIPMENT_CMD;
                    add_equipment_cmd.CommandType = CommandType.StoredProcedure;

                    add_equipment_cmd.Parameters.AddWithValue("@EQUIPMENTNAME", name);
                    add_equipment_cmd.Parameters.AddWithValue("@COST", cost);
                    add_equipment_cmd.Parameters.AddWithValue("@WEIGHT", weight);
                    add_equipment_cmd.Parameters.AddWithValue("@DESCRIPTION", description);

                    int rows = add_equipment_cmd.ExecuteNonQuery();

                    response = rows > 0
                                   ? $"Successfully added {name} to the equipment library!"
                                   : $"{name} is already in the equipment library!";
                }
            }

            Console.WriteLine($"{DateTime.Now.ToFileTime()} - Add Equipment - {name}");
            return response;
        }

        public static string RemoveEquipment(string name)
        {
            name = name.ToLower();

            string response;

            using (MySqlConnection conn = new MySqlConnection(ServerInfo.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand remove_equipment_cmd = new MySqlCommand())
                {
                    remove_equipment_cmd.Connection = conn;
                    remove_equipment_cmd.CommandText = REMOVE_EQUIPMENT_CMD;
                    remove_equipment_cmd.CommandType = CommandType.StoredProcedure;

                    remove_equipment_cmd.Parameters.AddWithValue("@EQUIPMENTNAME", name);

                    int rows = remove_equipment_cmd.ExecuteNonQuery();

                    response = rows > 0
                                   ? $"Successfully removed {name} from the equipment library!"
                                   : $"Could not find {name} in the equipment library!";
                }
            }

            Console.WriteLine($"{DateTime.Now.ToFileTime()} - Remove Equipment - {name}");
            return response;
        }

        public static string GetEquipmentInfo(string name)
        {
            name = name.ToLower();

            string response;

            using (MySqlConnection conn = new MySqlConnection(ServerInfo.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand get_equipment_cmd = new MySqlCommand())
                {
                    get_equipment_cmd.Connection = conn;
                    get_equipment_cmd.CommandText = GET_EQUIPMENT_CMD;
                    get_equipment_cmd.CommandType = CommandType.StoredProcedure;

                    get_equipment_cmd.Parameters.AddWithValue("@EQUIPMENTNAME", name);

                    MySqlDataReader reader = get_equipment_cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        StringBuilder builder = new StringBuilder();

                        builder.AppendLine($"{reader["equipment_name"]}");
                        builder.AppendLine($"{reader["description"]}");
                        builder.AppendLine($"Cost: {reader["cost"]}");
                        builder.AppendLine($"Weight: {reader["weight"]}");

                        response = builder.ToString();
                    }
                    else
                        response = $"Could not find {name} in the equipment library!";
                }
            }

            Console.WriteLine($"{DateTime.Now.ToFileTime()} - Get Equipment - {name}");
            return response;
        }

        public static string SearchEquipment(string query)
        {
            query = query.ToLower();

            string response;

            using (MySqlConnection conn = new MySqlConnection(ServerInfo.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand search_equipment_cmd = new MySqlCommand())
                {
                    search_equipment_cmd.Connection = conn;
                    search_equipment_cmd.CommandText = SEARCH_EQUIPMENT_CMD;
                    search_equipment_cmd.CommandType = CommandType.StoredProcedure;

                    search_equipment_cmd.Parameters.AddWithValue("@SEARCH", query);

                    MySqlDataReader reader = search_equipment_cmd.ExecuteReader();

                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine($"Search Results: {query}");

                    while (reader.Read())
                    {
                        builder.AppendLine($"{reader["equipment_name"]}");
                    }

                    response = builder.ToString();
                }
            }

            Console.WriteLine($"{DateTime.Now.ToFileTime()} - Search Equipment - {query}");
            return response;
        }
    }
}
