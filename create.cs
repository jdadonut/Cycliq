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
        private DiscordRole[] role_blank = new DiscordRole[] { };
        private DiscordMember[] member_blank = new DiscordMember[] { };
        [Command("category"), Description("make a new category!"), RequireBotPermissions(Permissions.ManageChannels), RequireUserPermissions(Permissions.ManageChannels)]
        public async Task Category(CommandContext ctx, string name, bool priv = false)
        {
            await ctx.TriggerTypingAsync();
            DiscordRole[] overroles = new DiscordRole[] { };
            DiscordOverwriteBuilder[] overrides = new DiscordOverwriteBuilder[] { };
            if (priv) {
                foreach (var i in ctx.Guild.Roles)
                    if (!i.Value.Permissions.HasPermission(Permissions.Administrator))
                    {
                        overroles.SetValue(i.Value, overroles.Length);
                        Console.WriteLine("a");
                    }
                foreach (var i in overroles)
                    overrides.SetValue(new DiscordOverwriteBuilder().For(i).Allow(Permissions.None), overrides.Length);
            }
            await ctx.Guild.CreateChannelCategoryAsync(name, overrides, reason: $"Action preformed by {ctx.Member.Id}");
            await ctx.RespondAsync($"Category {name} has been created!");
        }
        [Command("category"), Description("make a new category!"), RequireBotPermissions(Permissions.ManageChannels), RequireUserPermissions(Permissions.ManageChannels)]
        public async Task Category(CommandContext ctx, string name, bool priv = false, params DiscordRole[]? privToRoles)
        {
            privToRoles = privToRoles ?? new DiscordRole[0];
            await ctx.TriggerTypingAsync();
            DiscordRole[] overroles = new DiscordRole[] { };
            DiscordOverwriteBuilder[] overrides = new DiscordOverwriteBuilder[] { };
            if (priv)
            {
                foreach (var i in ctx.Guild.Roles)
                    if (!i.Value.Permissions.HasPermission(Permissions.Administrator) && !privToRoles.Contains(i.Value))
                    {
                        overroles.SetValue(i.Value, overroles.Length);
                        Console.WriteLine("a");
                    }
                foreach (var i in overroles)
                    overrides.SetValue(new DiscordOverwriteBuilder().For(i).Allow(Permissions.None), overrides.Length);
            }
            await ctx.Guild.CreateChannelCategoryAsync(name, overrides, reason: $"Action preformed by {ctx.Member.Id}");
            await ctx.RespondAsync($"Category {name} has been created!");

        }
        [Command("category"), Description("make a new category!"), RequireBotPermissions(Permissions.ManageChannels), RequireUserPermissions(Permissions.ManageChannels)]
        public async Task Category(CommandContext ctx, string name, bool priv = false, params DiscordMember[]? privToMembers)
        {
            privToMembers = privToMembers ?? new DiscordMember[0];
            await ctx.TriggerTypingAsync();
            DiscordRole[] overroles = new DiscordRole[] { };
            DiscordOverwriteBuilder[] overrides = new DiscordOverwriteBuilder[] { };
            if (priv)
            {
                foreach (var i in ctx.Guild.Roles)
                    if (!i.Value.Permissions.HasPermission(Permissions.Administrator))
                    {
                        overroles.SetValue(i.Value, overroles.Length);
                        Console.WriteLine("a");
                    }
                foreach (var i in overroles)
                    overrides.SetValue(new DiscordOverwriteBuilder().For(i).Allow(Permissions.None), overrides.Length);
                foreach (var i in privToMembers)
                    overrides.SetValue(new DiscordOverwriteBuilder().For(i).Allow(Permissions.SendMessages), overrides.Length);
            }
            await ctx.Guild.CreateChannelCategoryAsync(name, overrides, reason: $"Action preformed by {ctx.Member.Id}");
            await ctx.RespondAsync($"Category {name} has been created!");
        }
        [Command("category"), Description("make a new category!"), RequireBotPermissions(Permissions.ManageChannels), RequireUserPermissions(Permissions.ManageChannels)]
        public async Task Category(CommandContext ctx, string name, bool priv = false, params SnowflakeObject[]? privTo)
        {
            privTo = privTo ?? new SnowflakeObject[0];
            await ctx.TriggerTypingAsync();
            DiscordRole[] overroles = new DiscordRole[] { };
            DiscordOverwriteBuilder[] overrides = new DiscordOverwriteBuilder[] { };
            if (priv)
            {
                foreach (var i in ctx.Guild.Roles)
                    if (!i.Value.Permissions.HasPermission(Permissions.Administrator) && !privTo.Contains(i.Value))
                        overroles.SetValue(i.Value, overroles.Length);
                foreach (var i in overroles)
                    overrides.SetValue(new DiscordOverwriteBuilder().For(i).Allow(Permissions.None), overrides.Length);
                foreach (var i in privTo.OfType<DiscordMember>())
                    overrides.SetValue(new DiscordOverwriteBuilder().For(i).Allow(Permissions.SendMessages), overrides.Length);
            }
            await ctx.Guild.CreateChannelCategoryAsync(name, overrides, reason: $"Action preformed by {ctx.Member.Id}");
            await ctx.RespondAsync($"Category {name} has been created!");
        }
    }
}
