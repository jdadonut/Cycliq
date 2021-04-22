using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable

namespace Cycliq.Moderation
{
    public class Snipe : BaseCommandModule
    {
        [Command("snipe"), Description("snipes messages deleted in the past 30 seconds"), RequireUserPermissions(DSharpPlus.Permissions.ManageMessages)]
        [RequireGuild]
        public async Task SnipeCommand(CommandContext ctx)
        {

            Task.Run(async () =>
            {
                await ctx.TriggerTypingAsync();

                Dictionary<ulong, DiscordMessage> assumed = ctx.Services.GetService<SnipeManager>().FindMessagesByServer(ctx.Guild.Id);
                if (assumed == null || assumed.Count == 0)
                {
                    ctx.RespondAsync("No Deleted Messages in the past 30 seconds.");
                    return;
                }
                DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
                foreach (DiscordMessage msg in assumed.Values)
                    try
                    {
                        if (msg.Content.Trim().Length > 1945)
                            embed
                                .AddField($"{msg.Author.Username}#{msg.Author.Discriminator} (deleted) in #{msg.Channel.Name}", msg.Content.Trim().Remove(1945) + $"{(msg.Attachments.Count != 0 ? "(+ 📎)" : "")}");
                        else
                            embed
                                .AddField($"{msg.Author.Username}#{msg.Author.Discriminator} (deleted) in #{msg.Channel.Name}", msg.Content.Trim() + $"{(msg.Attachments.Count != 0 ? "(+ 📎)" : "")}");

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"[Cycliq] | [ERR] => Encountered Exception {e.Message} while running cq!snipe");
                        ctx.RespondAsync(embed: embed);
                        return;
                    }
                ctx.RespondAsync(embed);

            });
        }
    }
}