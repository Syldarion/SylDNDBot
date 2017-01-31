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
    public static class SpellLibrary
    {
        private const string GET_SPELL_INFO_CMD = "get_spell";
        private const string ADD_SPELL_INFO_CMD = "add_spell";
        private const string REM_SPELL_INFO_CMD = "remove_spell";
        private const string SEARCH_SPELLS_CMD = "search_spells";

        public static string AddSpell(string name, int level, string school, string castTime, string range,
            string components, string materials, string duration, string description)
        {
            name = name.ToLower();

            string response;

            using(MySqlConnection conn = new MySqlConnection(ServerInfo.ConnectionString))
            {
                conn.Open();
                using(MySqlCommand add_spell_cmd = new MySqlCommand())
                {
                    add_spell_cmd.Connection = conn;
                    add_spell_cmd.CommandText = ADD_SPELL_INFO_CMD;
                    add_spell_cmd.CommandType = CommandType.StoredProcedure;

                    add_spell_cmd.Parameters.AddWithValue("@SPELLNAME", name);
                    add_spell_cmd.Parameters.AddWithValue("@SPELLLEVEL", level);
                    add_spell_cmd.Parameters.AddWithValue("@SCHOOL", school);
                    add_spell_cmd.Parameters.AddWithValue("@CAST", castTime);
                    add_spell_cmd.Parameters.AddWithValue("@SPELLRANGE", range);
                    add_spell_cmd.Parameters.AddWithValue("@COMPONENTS", components);
                    add_spell_cmd.Parameters.AddWithValue("@MATERIALS", materials);
                    add_spell_cmd.Parameters.AddWithValue("@DURATION", duration);
                    add_spell_cmd.Parameters.AddWithValue("@DESCRIPTION", description);

                    add_spell_cmd.Parameters.Add("@RESULTMESSAGE", MySqlDbType.VarChar);
                    add_spell_cmd.Parameters.Add("@SPELLID", MySqlDbType.Int32);
                    add_spell_cmd.Parameters["@RESULTMESSAGE"].Direction = ParameterDirection.Output;
                    add_spell_cmd.Parameters["@SPELLID"].Direction = ParameterDirection.Output;

                    add_spell_cmd.ExecuteNonQuery();

                    response = Convert.ToString(add_spell_cmd.Parameters["@RESULTMESSAGE"].Value);
                }
            }

            Console.WriteLine($"{DateTime.Now.ToFileTime()} - Add Spell - {name}");
            return response;
        }

        public static string RemoveSpell(string name)
        {
            name = name.ToLower();

            string response;

            using(MySqlConnection conn = new MySqlConnection(ServerInfo.ConnectionString))
            {
                conn.Open();
                using(MySqlCommand remove_spell_cmd = new MySqlCommand())
                {
                    remove_spell_cmd.Connection = conn;
                    remove_spell_cmd.CommandText = REM_SPELL_INFO_CMD;
                    remove_spell_cmd.CommandType = CommandType.StoredProcedure;

                    remove_spell_cmd.Parameters.AddWithValue("@SPELLNAME", name);

                    remove_spell_cmd.ExecuteNonQuery();

                    response = $"Successfully removed {name} from the spell library!";
                }
            }

            Console.WriteLine($"{DateTime.Now.ToFileTime()} - Remove Spell - {name}");
            return response;
        }

        public static string GetSpellInfo(string name)
        {
            name = name.ToLower();

            string response;

            using(MySqlConnection conn = new MySqlConnection(ServerInfo.ConnectionString))
            {
                conn.Open();
                using(MySqlCommand get_spell_cmd = new MySqlCommand())
                {
                    get_spell_cmd.Connection = conn;
                    get_spell_cmd.CommandText = GET_SPELL_INFO_CMD;
                    get_spell_cmd.CommandType = CommandType.StoredProcedure;

                    get_spell_cmd.Parameters.AddWithValue("@SPELLNAME", name);

                    MySqlDataReader reader = get_spell_cmd.ExecuteReader();

                    if(reader.Read())
                    {
                        StringBuilder builder = new StringBuilder();

                        builder.AppendLine($"{reader["spell_name"]}");
                        builder.AppendLine($"Level: {reader["spell_level"]}");
                        builder.AppendLine($"School: {reader["school"]}");
                        builder.AppendLine($"Cast Time: {reader["cast_time"]}");
                        builder.AppendLine($"Range: {reader["spell_range"]}");
                        builder.AppendLine($"Components: {reader["components"]}");
                        builder.AppendLine($"Materials: {reader["materials"]}");
                        builder.AppendLine($"Duration: {reader["duration"]}");
                        builder.AppendLine($"Description: {reader["description"]}");

                        response = builder.ToString();
                    }
                    else
                        response = $"Could not find {name} in the spell library!";
                }
            }

            Console.WriteLine($"{DateTime.Now.ToFileTime()} - Get Spell - {name}");
            return response;
        }

        public static string SearchSpells(string query)
        {
            query = query.ToLower();

            string response;

            using(MySqlConnection conn = new MySqlConnection(ServerInfo.ConnectionString))
            {
                conn.Open();
                using(MySqlCommand search_cmd = new MySqlCommand())
                {
                    search_cmd.Connection = conn;
                    search_cmd.CommandText = SEARCH_SPELLS_CMD;
                    search_cmd.CommandType = CommandType.StoredProcedure;

                    search_cmd.Parameters.AddWithValue("@SEARCH", query);

                    MySqlDataReader reader = search_cmd.ExecuteReader();

                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine($"Search Results: {query}");

                    while(reader.Read())
                    {
                        builder.AppendLine($"{reader["spell_name"]}");
                    }

                    response = builder.ToString();
                }
            }

            Console.WriteLine($"{DateTime.Now.ToFileTime()} - Search Spells - {query}");
            return response;
        }
    }
}
