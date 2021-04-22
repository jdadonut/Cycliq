using System;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Attributes;
using System.Collections.Generic;
using Cycliq.Converters;
using System.Linq;
using DSharpPlus.CommandsNext.Entities;

namespace Cycliq.Generators
{
    public class HelpGenerator : BaseHelpFormatter
    {
        protected DiscordEmbedBuilder _embed;
        public HelpGenerator(CommandContext context) : base(context)
        {
            _embed = new DiscordEmbedBuilder();

        }
        public override BaseHelpFormatter WithCommand(Command command)
        {
            string category;
            string usage;

            List<Attribute> attributes = Converter.GetMutableAttributesList( command.CustomAttributes );
            CategoryAttribute categoryAttribute = attributes.OfType<CategoryAttribute>().FirstOrDefault();
            UsageAttribute usageAttribute = attributes.OfType<UsageAttribute>().FirstOrDefault();
            category = $"{categoryAttribute.Category}::{categoryAttribute.Subcategory}";
            usage = usageAttribute.Usage;


            _embed
                .WithTitle(command.Name)
                .WithDescription(command.Description)
                .AddField("Category", category, true)
                .AddField("Usage", usage, true);
            return this;
        }

        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {
            foreach (Command command in subcommands)
            {
                string category;
                string usage;

                List<Attribute> attributes = Converter.GetMutableAttributesList( command.CustomAttributes );
                CategoryAttribute categoryAttribute = attributes.OfType<CategoryAttribute>().FirstOrDefault();
                UsageAttribute usageAttribute = attributes.OfType<UsageAttribute>().FirstOrDefault();
                category = $"{categoryAttribute.Category}::{categoryAttribute.Subcategory}";
                usage = usageAttribute.Usage;

                _embed
                    .AddField(command.Name, $"{command.Description}\nCategory: {category}\nUsage: {usage}");
            }
            return this;
        }
        public override CommandHelpMessage Build()
        {
            _embed.Color = DiscordColor.SpringGreen;
            return new CommandHelpMessage(embed:_embed);
        }

    }
}