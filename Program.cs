﻿using System;
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
            var bot = new DiscordBot();
            bot.StartBot();
        }
    }
}
