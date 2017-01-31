﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Net;
using Discord.Commands;

namespace SylDNDBot
{
    public class DiscordBot
    {
        public DiscordClient Client { get; }

        private CommandService comService;

        public DiscordBot()
        {
            Client = new DiscordClient();

            var com_conf_builder = new CommandServiceConfigBuilder();
            com_conf_builder.PrefixChar = '!';
            com_conf_builder.AllowMentionPrefix = false;
            com_conf_builder.IsSelfBot = false;
            com_conf_builder.HelpMode = HelpMode.Public;

            var com_service_conf = com_conf_builder.Build();

            comService = new CommandService(com_service_conf);
            PopulateCommands();

            Client.AddService(comService);
        }

        public void StartBot()
        {
            Client.ExecuteAndWait(async () => { await Client.Connect("syldarion.0@gmail.com", "e8e6755f50"); });
        }

        public void PopulateCommands()
        {
            comService.CreateGroup("spell", cgb =>
            {
                cgb.CreateCommand("info")
                    .Parameter("NAME")
                    .Description("Returns information on a given spell")
                    .Do(
                        async e =>
                        {
                            string result = SpellLibrary.GetSpellInfo(e.GetArg("NAME"));
                            await e.Channel.SendMessage(result);
                        });
                cgb.CreateCommand("search")
                    .Parameter("QUERY")
                    .Description("Searches for spells")
                    .Do(
                        async e =>
                        {
                            string result = SpellLibrary.SearchSpells(e.GetArg("QUERY"));
                            await e.Channel.SendMessage(result);
                        });
                cgb.CreateCommand("add")
                    .Parameter("NAME")
                    .Parameter("LEVEL")
                    .Parameter("SCHOOL")
                    .Parameter("CASTTIME")
                    .Parameter("RANGE")
                    .Parameter("COMPONENTS")
                    .Parameter("MATERIALS")
                    .Parameter("DURATION")
                    .Parameter("DESCRIPTION")
                    .Description("Adds a new spell to the spell library")
                    .Do(
                        async e =>
                        {
                            string result = SpellLibrary.AddSpell(
                                e.GetArg("NAME"),
                                int.Parse(e.GetArg("LEVEL")),
                                e.GetArg("SCHOOL"),
                                e.GetArg("CASTTIME"),
                                e.GetArg("RANGE"),
                                e.GetArg("COMPONENTS"),
                                e.GetArg("MATERIALS"),
                                e.GetArg("DURATION"),
                                e.GetArg("DESCRIPTION"));

                            await e.Channel.SendMessage(result);
                        });
                cgb.CreateCommand("remove")
                    .Parameter("NAME")
                    .Description("Removes a spell from the spell library")
                    .Do(async e =>
                    {
                        string result = SpellLibrary.RemoveSpell(e.GetArg("NAME"));
                        await e.Channel.SendMessage(result);
                    });
            });

            comService.CreateGroup("trait", cgb =>
            {
                cgb.CreateCommand("info")
                    .Parameter("NAME")
                    .Description("Returns information on a given trait")
                    .Do(
                        async e =>
                        {
                            string result = TraitLibrary.GetTraitInfo(e.GetArg("NAME"));
                            await e.Channel.SendMessage(result);
                        });
                cgb.CreateCommand("search")
                    .Parameter("QUERY")
                    .Description("Searches for traits")
                    .Do(
                        async e =>
                        {
                            string result = TraitLibrary.SearchTraits(e.GetArg("QUERY"));
                            await e.Channel.SendMessage(result);
                        });
                cgb.CreateCommand("add")
                    .Parameter("NAME")
                    .Parameter("DESCRIPTION")
                    .Description("Adds a trait to the trait library")
                    .Do(
                        async e =>
                        {
                            Trait new_trait = new Trait(e.GetArg("NAME"), e.GetArg("DESCRIPTION"));
                            TraitLibrary.AddTrait(new_trait);
                            await e.Channel.SendMessage($"Successfully added {new_trait.Name} to the trait library!");
                        });
                cgb.CreateCommand("remove")
                    .Parameter("NAME")
                    .Description("Removes a trait from the trait library")
                    .Do(async e =>
                    {
                        TraitLibrary.RemoveTrait(e.GetArg("NAME"));
                        await
                            e.Channel.SendMessage($"Successfully removed {e.GetArg("NAME")} from the trait library!");
                    });
                cgb.CreateCommand("edit")
                    .Parameter("NAME")
                    .Parameter("VALUE")
                    .Description("Edits information on a given trait")
                    .Do(
                        async e =>
                        {
                            string result = TraitLibrary.EditTraitInfo(e.GetArg("NAME"), e.GetArg("VALUE"));
                            await e.Channel.SendMessage(result);
                        });
            });

            comService.CreateGroup("equipment", cgb =>
            {
                cgb.CreateCommand("info")
                    .Parameter("NAME")
                    .Parameter("FIELD")
                    .Description("Returns information on given equipment")
                    .Do(
                        async e =>
                        {
                            string result = EquipmentLibrary.GetEquipmentInfo(e.GetArg("NAME"), e.GetArg("FIELD"));
                            await e.Channel.SendMessage(result);
                        });
                cgb.CreateCommand("search")
                    .Parameter("QUERY")
                    .Description("Searches for spells")
                    .Do(
                        async e =>
                        {
                            string result = EquipmentLibrary.SearchEquipment(e.GetArg("QUERY"));
                            await e.Channel.SendMessage(result);
                        });
                cgb.CreateCommand("add")
                    .Parameter("NAME")
                    .Parameter("COST")
                    .Parameter("WEIGHT")
                    .Parameter("DESCRIPTION")
                    .Description("Adds a new item to the equipment library")
                    .Do(
                        async e =>
                        {
                            Equipment new_equipment = new Equipment(
                                e.GetArg("NAME"),
                                e.GetArg("COST"),
                                e.GetArg("WEIGHT"),
                                e.GetArg("DESCRIPTION"));

                            EquipmentLibrary.AddEquipment(new_equipment);

                            await
                                e.Channel.SendMessage(
                                    $"Successfully added {new_equipment.Name} to the equipment library!");
                        });
                cgb.CreateCommand("remove")
                    .Parameter("NAME")
                    .Description("Removes an item from the equipment library")
                    .Do(async e =>
                    {
                        EquipmentLibrary.RemoveEquipment(e.GetArg("NAME"));
                        await
                            e.Channel.SendMessage($"Successfully removed {e.GetArg("NAME")} from the equipment library!");
                    });
                cgb.CreateCommand("edit")
                    .Parameter("NAME")
                    .Parameter("FIELD")
                    .Parameter("VALUE")
                    .Description("Edits information on given equipment")
                    .Do(
                        async e =>
                        {
                            string result = EquipmentLibrary.EditEquipmentInfo(e.GetArg("NAME"), e.GetArg("FIELD"),
                                e.GetArg("VALUE"));
                            await e.Channel.SendMessage(result);
                        });
            });

            comService.CreateGroup("definition", cgb =>
            {
                cgb.CreateCommand("info")
                    .Parameter("NAME")
                    .Description("Returns definition of a given term")
                    .Do(
                        async e =>
                        {
                            string result = DefinitionLibrary.GetDefinition(e.GetArg("NAME"));
                            await e.Channel.SendMessage(result);
                        });
                cgb.CreateCommand("search")
                    .Parameter("QUERY")
                    .Description("Searches for terms")
                    .Do(
                        async e =>
                        {
                            string result = DefinitionLibrary.SearchTerms(e.GetArg("QUERY"));
                            await e.Channel.SendMessage(result);
                        });
                cgb.CreateCommand("add")
                    .Parameter("NAME")
                    .Parameter("DEFINITION")
                    .Description("Adds a new definition to the definition library")
                    .Do(
                        async e =>
                        {
                            DefinitionLibrary.AddDefinition(e.GetArg("NAME"), e.GetArg("DEFINITION"));
                            await
                                e.Channel.SendMessage(
                                    $"Successfully added {e.GetArg("NAME")} to the definition library!");
                        });
                cgb.CreateCommand("remove")
                    .Parameter("NAME")
                    .Description("Removes a term from the definition library")
                    .Do(async e =>
                    {
                        DefinitionLibrary.RemoveDefinition(e.GetArg("NAME"));
                        await
                            e.Channel.SendMessage(
                                $"Successfully removed {e.GetArg("NAME")} from the definition library!");
                    });
                cgb.CreateCommand("edit")
                    .Parameter("NAME")
                    .Parameter("VALUE")
                    .Description("Edits information on a given term")
                    .Do(
                        async e =>
                        {
                            string result = DefinitionLibrary.EditDefinition(e.GetArg("NAME"), e.GetArg("VALUE"));
                            await e.Channel.SendMessage(result);
                        });
            });

            comService.CreateCommand("roll")
                .Parameter("INPUT")
                .Description("Rolls dice")
                .Do(async e =>
                {
                    await e.Channel.SendMessage(DNDUtilities.Roll(e.GetArg("INPUT")));
                });
        }
    }
}