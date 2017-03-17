using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SylDNDBot
{
    public static class TraitLibrary
    {
        public const string GET_TRAIT_CMD = "get_trait";
        public const string ADD_TRAIT_CMD = "add_trait";
        public const string REMOVE_TRAIT_CMD = "remove_trait";
        public const string SEARCH_TRAITS_CMD = "search_traits";

        public static string AddTrait(string name, string description)
        {
            name = name.ToLower();

            string response;

            using(MySqlConnection conn = new MySqlConnection(ServerInfo.ConnectionString))
            {
                conn.Open();
                using(MySqlCommand add_trait_cmd = new MySqlCommand())
                {
                    add_trait_cmd.Connection = conn;
                    add_trait_cmd.CommandText = ADD_TRAIT_CMD;
                    add_trait_cmd.CommandType = CommandType.StoredProcedure;

                    add_trait_cmd.Parameters.AddWithValue("@TRAITNAME", name);
                    add_trait_cmd.Parameters.AddWithValue("@DESCRIPTION", description);

                    int rows = add_trait_cmd.ExecuteNonQuery();

                    response = rows > 0
                                   ? $"Successfully added {name} to the trait library!"
                                   : $"{name} is already in the trait library!";
                }
            }

            Console.WriteLine($"{DateTime.Now.ToFileTime()} - Add Trait - {name}");
            return response;
        }

        public static string RemoveTrait(string name)
        {
            name = name.ToLower();

            string response;

            using(MySqlConnection conn = new MySqlConnection(ServerInfo.ConnectionString))
            {
                conn.Open();
                using(MySqlCommand remove_trait_cmd = new MySqlCommand())
                {
                    remove_trait_cmd.Connection = conn;
                    remove_trait_cmd.CommandText = REMOVE_TRAIT_CMD;
                    remove_trait_cmd.CommandType = CommandType.StoredProcedure;

                    remove_trait_cmd.Parameters.AddWithValue("@TRAITNAME", name);

                    int rows = remove_trait_cmd.ExecuteNonQuery();

                    response = rows > 0
                                   ? $"Successfully removed {name} from the trait library!"
                                   : $"Could not find {name} in the trait library!";
                }
            }

            Console.WriteLine($"{DateTime.Now.ToFileTime()} - Remove Trait - {name}");
            return response;
        }

        public static string GetTraitInfo(string name)
        {
            name = name.ToLower();

            string response;

            using(MySqlConnection conn = new MySqlConnection(ServerInfo.ConnectionString))
            {
                conn.Open();
                using(MySqlCommand get_trait_cmd = new MySqlCommand())
                {
                    get_trait_cmd.Connection = conn;
                    get_trait_cmd.CommandText = GET_TRAIT_CMD;
                    get_trait_cmd.CommandType = CommandType.StoredProcedure;

                    get_trait_cmd.Parameters.AddWithValue("@TRAITNAME", name);

                    MySqlDataReader reader = get_trait_cmd.ExecuteReader();

                    if(reader.Read())
                    {
                        StringBuilder builder = new StringBuilder();

                        builder.AppendLine($"{reader["trait_name"]}");
                        builder.AppendLine($"{reader["description"]}");

                        response = builder.ToString();
                    }
                    else
                        response = $"Could not find {name} in the trait library!";
                }
            }

            Console.WriteLine($"{DateTime.Now.ToFileTime()} - Get Trait - {name}");
            return response;
        }

        public static string SearchTraits(string query)
        {
            query = query.ToLower();

            string response;

            using(MySqlConnection conn = new MySqlConnection(ServerInfo.ConnectionString))
            {
                conn.Open();
                using(MySqlCommand search_traits_cmd = new MySqlCommand())
                {
                    search_traits_cmd.Connection = conn;
                    search_traits_cmd.CommandText = SEARCH_TRAITS_CMD;
                    search_traits_cmd.CommandType = CommandType.StoredProcedure;

                    search_traits_cmd.Parameters.AddWithValue("@SEARCH", query);

                    MySqlDataReader reader = search_traits_cmd.ExecuteReader();
                    
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine($"Search Results: {query}");

                    while(reader.Read())
                    {
                        builder.AppendLine($"{reader["trait_name"]}");
                    }

                    response = builder.ToString();
                }
            }

            Console.WriteLine($"{DateTime.Now.ToFileTime()} - Search Traits - {query}");
            return response;
        }
    }
}