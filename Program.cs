using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Net;

namespace SylDNDBot
{
    class Program
    {
        static void Main(string[] args)
        {
            SpellLibrary.AddSpell(new Spell(
                                      "Ray of Frost",
                                      0,
                                      "Evocation",
                                      "1 action",
                                      "60 feet",
                                      SpellComponent.Verbal | SpellComponent.Somatic,
                                      null,
                                      "Instantaneous",
                                      "N/A"));

            var client = new DiscordClient();
            var parser = new CommandParser();

            client.AddService(parser.ComService);

            client.ExecuteAndWait(async () =>
            {
                await client.Connect("syldarion.0@gmail.com", "e8e6755f50");
            });
        }
    }
}
