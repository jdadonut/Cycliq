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
    class ModerationCommands : IModule
    {
        [Command("ban")]
        [Description("bans a user")]
        [RequirePermissions(DSharpPlus.Permissions.BanMembers)]
        [RequireUserPermissions(DSharpPlus.Permissions.BanMembers)]
        public async Task Ban(CommandContext ctx, string mention,[RemainingText]string reason)
        {
            await ctx.RespondAsync("This command is not implemented yet.");
            return;
            await ctx.TriggerTypingAsync();

            // Extract UID from string mention
            while (true)
            {
                if (!new Regex("(<@|<@!)?[0-9]{1,30}>?").IsMatch(mention))
                {
                    #pragma warning disable CS4014 
                    awaitdel(await ctx.RespondAsync($"{mention} isn't a valid tag! Send a valid tag to ban that person!"), 16000);
                    #pragma warning restore CS4014 
                    var message = await ctx.Client.GetInteractivityModule().WaitForMessageAsync(
                        c => c.Author.Id == ctx.Message.Author.Id,
                        TimeSpan.FromSeconds(15));
                    if (message == null || message.Message.Content.ToLower() == "cancel")
                    {
                        await ctx.RespondAsync("Command Cancelled.");
                        return;
                    }
                    else
                        mention = message.Message.Content;

                }
                else
                    break;

            }
            if (mention.StartsWith("<@!"))
                mention = mention.Substring(2);
            if (mention.StartsWith("<@"))
                mention = mention.Substring(1);
            if (mention.EndsWith(">"))
                mention.Remove(mention.Length - 1);
            // {mention} now either
            // a) can be flashed to an int
            // b) is not a user mention.
            try { int.Parse(mention); } catch (Exception e) { await ctx.RespondAsync("Probably not a valid mention"); return; }
            DiscordMember? usr = await ctx.Guild.GetMemberAsync((ulong)int.Parse(mention));
            try
            {
                if (usr == null)
                    await ctx.Guild.BanMemberAsync((ulong)int.Parse(mention), 0, reason);
            }
            catch (Exception e)
            {
                await ctx.RespondAsync("An error occurred, it was logged.");
            }
        }

        private async Task awaitdel(DiscordMessage discordMessage, int ms)
        {
            await Task.Delay(ms);
            try { await discordMessage.DeleteAsync(); } 
            catch (Exception e) { Console.WriteLine($"Deleting message with ID {discordMessage.Id} failed. Reason: {e.Message}"); }

        }
    }
}
