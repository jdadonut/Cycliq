using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus;
using DSharpPlus.Interactivity;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
namespace Cycliq
{
    [Group("new")]
    class Create : BaseCommandModule
    {
        [Command("category"), RequireBotPermissions(Permissions.ManageChannels), RequireUserPermissions(Permissions.ManageChannels)]
        public async Task Category(CommandContext ctx, [RemainingText]string name, params SnowflakeObject[] privTo)
        {
            privTo = privTo ?? new SnowflakeObject[0];
            await ctx.TriggerTypingAsync();
            DiscordRole[] overroles = new DiscordRole[] { };
            DiscordOverwriteBuilder[] overrides = new DiscordOverwriteBuilder[] { };
            if (privTo.Count() != 0)
            {
                foreach (var i in ctx.Guild.Roles)
                    if (!i.Value.Permissions.HasPermission(Permissions.Administrator) && !privTo.Contains(i.Value))
                        overroles.SetValue(i.Value, overroles.Length);
                foreach (var i in overroles)
                    overrides.SetValue(new DiscordOverwriteBuilder().For(i).Allow(Permissions.None), overrides.Length);
                foreach (var i in privTo.OfType<DiscordMember>())
                    overrides.SetValue(new DiscordOverwriteBuilder().For(i).Allow(Permissions.SendMessages), overrides.Length);
            };
            await ctx.Guild.CreateChannelCategoryAsync(name, overrides, reason: $"Action preformed by {ctx.Member.Id}");
            await ctx.RespondAsync($"Category {name} has been created!");
        }
        [Command("role"), RequireUserPermissions(Permissions.ManageRoles), RequireBotPermissions(Permissions.ManageRoles)]
        public async Task Role(CommandContext ctx, [RemainingText]string name, DiscordColor? color, bool hoisted=false, bool mentionable=false)
        {
            await ctx.Guild.CreateRoleAsync(
                name,
                color: color,
                hoist:hoisted,
                mentionable: mentionable,
                reason: $"Action done by {ctx.Member.DisplayName}"
            );
            await ctx.RespondAsync("Created role " + name + "!");
        }
    }
}
