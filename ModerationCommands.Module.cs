using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
namespace Cycliq
{
    class ModerationCommands : BaseCommandModule
    {
        [Command("ban")]
        [Description("bans a user")]
        [RequirePermissions(DSharpPlus.Permissions.BanMembers)]
        [RequireUserPermissions(DSharpPlus.Permissions.BanMembers)]
        public async Task Ban(CommandContext ctx, DiscordUser usr, [RemainingText] string reason)
        {
            await ctx.RespondAsync("This command is not implemented yet.");
            return;
            /*
            await ctx.TriggerTypingAsync();

            await ctx.RespondAsync("An error occurred, it was logged.");

            int BanningPosition = 9999;
            int ModPosition = 9999;
            foreach (DiscordRole x in usr.Roles)
                if (x.Position > BanningPosition)
                    BanningPosition = x.Position;
            foreach (DiscordRole x in (await ctx.Guild.GetMemberAsync(ctx.Message.Author.Id)).Roles)
                if (x.Position > ModPosition)
                    ModPosition = x.Position;
            if (BanningPosition > ModPosition)
            {
                try
                {
                    await usr.BanAsync(reason: reason);
                    await ctx.RespondAsync(embed: new DiscordEmbedBuilder().WithAuthor(ctx.Message.Author.Username, iconUrl: ctx.Message.Author.AvatarUrl).AddField("User Banned", usr.Username, true).AddField("Moderator", ctx.Message.Author.Username, false).WithImageUrl(usr.AvatarUrl).AddField("Ban Reason", reason));
                }
                catch (Exception e)
                {
                    await ctx.RespondAsync("This bot likely doesnt have the power to ban that person!");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }

            */
            }

            async Task awaitdel(DiscordMessage discordMessage, int ms)
            {
                await Task.Delay(ms);
                try { await discordMessage.DeleteAsync(); }
                catch (Exception e) { Console.WriteLine($"Deleting message with ID {discordMessage.Id} failed. Reason: {e.Message}"); }

            }
        }
    }

