using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Net;
using Discord.Commands;

namespace SylDNDBot
{
    public class CommandParser
    {
        public CommandService ComService { get; private set; }

        public CommandParser()
        {
            var command_config_builder = new CommandServiceConfigBuilder();
            command_config_builder.PrefixChar = '!';
            command_config_builder.AllowMentionPrefix = false;
            command_config_builder.IsSelfBot = false;
            command_config_builder.ExecuteHandler = CommandExecuteHandlerFunction;
            command_config_builder.ErrorHandler = CommandErrorHandlerFunction;
            command_config_builder.HelpMode = HelpMode.Disabled;

            var command_service_config = command_config_builder.Build();
            
            ComService = new CommandService(command_service_config);
        }

        public void PopulateCommands()
        {
            ComService.CreateCommand("spell")
                      .Parameter("NAME", ParameterType.Required)
                      .Parameter("FIELD", ParameterType.Required)
                      .Description("Returns information on a given spell")
                      .Do(
                          async e =>
                          {
                              string result = SpellLibrary.GetSpellInfo(e.GetArg("NAME"), e.GetArg("FIELD"));
                              await e.Channel.SendMessage(result);
                          });
        }

        public void CommandExecuteHandlerFunction(object sender, CommandEventArgs args)
        {
            
        }

        public void CommandErrorHandlerFunction(object sender, CommandErrorEventArgs args)
        {
            
        }
    }
}
