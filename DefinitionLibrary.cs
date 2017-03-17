using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SylDNDBot
{
    public static class DefinitionLibrary
    {
        private const string GET_DEFINITION_CMD = "get_definition";
        private const string ADD_DEFINITION_CMD = "add_definition";
        private const string REMOVE_DEFINITION_CMD = "remove_definition";
        private const string SEARCH_DEFINITIONS_CMD = "search_definitions";

        public static string AddDefinition(string name, string definition)
        {
            name = name.ToLower();

            string response;

            using (MySqlConnection conn = new MySqlConnection(ServerInfo.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand add_definition_cmd = new MySqlCommand())
                {
                    add_definition_cmd.Connection = conn;
                    add_definition_cmd.CommandText = ADD_DEFINITION_CMD;
                    add_definition_cmd.CommandType = CommandType.StoredProcedure;

                    add_definition_cmd.Parameters.AddWithValue("@TERMNAME", name);
                    add_definition_cmd.Parameters.AddWithValue("@DEFINITION", definition);

                    int rows = add_definition_cmd.ExecuteNonQuery();

                    response = rows > 0
                                   ? $"Successfully added {name} to the definition library!"
                                   : $"{name} is already in the definition library!";
                }
            }

            Console.WriteLine($"{DateTime.Now.ToFileTime()} - Add Definition - {name}");
            return response;
        }

        public static string RemoveDefinition(string name)
        {
            name = name.ToLower();

            string response;

            using (MySqlConnection conn = new MySqlConnection(ServerInfo.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand remove_definition_cmd = new MySqlCommand())
                {
                    remove_definition_cmd.Connection = conn;
                    remove_definition_cmd.CommandText = REMOVE_DEFINITION_CMD;
                    remove_definition_cmd.CommandType = CommandType.StoredProcedure;

                    remove_definition_cmd.Parameters.AddWithValue("@TERMNAME", name);

                    int rows = remove_definition_cmd.ExecuteNonQuery();

                    response = rows > 0
                                   ? $"Successfully removed {name} from the definition library!"
                                   : $"Could not find {name} in the definition library!";
                }
            }

            Console.WriteLine($"{DateTime.Now.ToFileTime()} - Remove Definition - {name}");
            return response;
        }

        public static string GetDefinition(string name)
        {
            name = name.ToLower();

            string response;

            using (MySqlConnection conn = new MySqlConnection(ServerInfo.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand get_defintiion_cmd = new MySqlCommand())
                {
                    get_defintiion_cmd.Connection = conn;
                    get_defintiion_cmd.CommandText = GET_DEFINITION_CMD;
                    get_defintiion_cmd.CommandType = CommandType.StoredProcedure;

                    get_defintiion_cmd.Parameters.AddWithValue("@TERMNAME", name);

                    MySqlDataReader reader = get_defintiion_cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        StringBuilder builder = new StringBuilder();

                        builder.AppendLine($"{reader["term_name"]}");
                        builder.AppendLine($"{reader["definition"]}");

                        response = builder.ToString();
                    }
                    else
                        response = $"Could not find {name} in the definition library!";
                }
            }

            Console.WriteLine($"{DateTime.Now.ToFileTime()} - Get Definition - {name}");
            return response;
        }

        public static string SearchTerms(string query)
        {
            query = query.ToLower();

            string response;

            using (MySqlConnection conn = new MySqlConnection(ServerInfo.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand search_definitions_cmd = new MySqlCommand())
                {
                    search_definitions_cmd.Connection = conn;
                    search_definitions_cmd.CommandText = SEARCH_DEFINITIONS_CMD;
                    search_definitions_cmd.CommandType = CommandType.StoredProcedure;

                    search_definitions_cmd.Parameters.AddWithValue("@SEARCH", query);

                    MySqlDataReader reader = search_definitions_cmd.ExecuteReader();

                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine($"Search Results: {query}");

                    while (reader.Read())
                    {
                        builder.AppendLine($"{reader["term_name"]}");
                    }

                    response = builder.ToString();
                }
            }

            Console.WriteLine($"{DateTime.Now.ToFileTime()} - Search Definitions - {query}");
            return response;
        }
    }
}
