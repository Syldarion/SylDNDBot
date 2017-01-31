using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylDNDBot
{
    public static class ServerInfo
    {
        private const string SERVER = "localhost";
        private const string DATABASE = "dnd_database";
        private const string UID = "root";
        private const string PASSWORD = "Itsureisaweirdworld1!";

        public static readonly string ConnectionString =
            $"server={SERVER};uid={UID};pwd={PASSWORD};database={DATABASE}";
    }
}
