using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
namespace Cycliq
{
    [Group("cycliq"), Aliases("cq", "c")]
    class DebugCommands : BaseCommandModule
    {
        [Group("set")]
        
        class Cycliq_set : BaseCommandModule
        {
            [Command("status")]
            [Description("Set Bot Status, Owner Only.")]
            [RequireOwner]
            public async Task setStatus(CommandContext ctx, [RemainingText] string Status)
            {
                await ctx.TriggerTypingAsync();
                await ctx.Client.UpdateStatusAsync(new DiscordActivity(Status));
                await ctx.RespondAsync($"Status set to \"{Status}\"!");
            }
            /*
            [Command("token")]
            [Description("no lmao")]
            public async Task setToken(CommandContext ctx, string token, [RemainingText]string s)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync("no?");
            }*/
        }

        [Group("get"), RequireGuild]
        class Cycliq_get : BaseCommandModule
        {

            [Command("token"), Description("Get the current bot token!")]
            public async Task getToken(CommandContext ctx, [RemainingText] string s)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync("wh-");
                await ctx.TriggerTypingAsync();
                await Task.Delay(1000);
                await ctx.RespondAsync("no?");
            }
            [Command("intro"), RequireOwner]
            public async Task getIntro(CommandContext c, DiscordChannel ch = null)
            {
                ch = ch ?? c.Channel;
                DiscordWebhook hook = null;
                await c.TriggerTypingAsync();
                hook = await ch.CreateWebhookAsync("[CYCLIQ BOT WEBHOOK 12m3ki4j2eoieboi3]", reason: "Command run: `get intro`");
                string[] images = new string[] { "https://send-me-femboy.thigh.pics/bdf88734799A79aC.png", "https://send-me-femboy.thigh.pics/Ce28e5Fbdc0B7ddA.png", "https://send-me-femboy.thigh.pics/6b2be89e057F79CF.png" };
                await hook.ExecuteAsync(new DiscordWebhookBuilder()
                    .WithAvatarUrl("https://send-me-femboy.thigh.pics/4243aFb25f53beB1.png")
                    .WithUsername("jai")
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithAuthor("jai, but as a webhook", "https://jaio.carrd.co", "https://send-me-femboy.thigh.pics/4243aFb25f53beB1.png")
                    .WithDescription(@"Languages i code in:
proficient: python, js, HTML, CSS, SCSS
not too bad: C#, Java
Somewhat Actively Learning: C#, Java, C++, Rust, Zig, SQL
Plan to learn: C, Haxe

[my youtube](https://www.youtube.com/channel/UCBY-ILM-Up8QXgz-hua0h9Q),  [my kofi](https://ko-fi.com/jdadonut/), [my anilist](https://anilist.co/user/jaio/),  [my steam](https://steamcommunity.com/id/jdadonut/),  [my keybase](https://keybase.io/jaio), [my github](https://github.com/jdadonut/), [my telegram](https://t.me/jai_jisj/)")
                    .WithTitle("your worst daydream")
                    .WithUrl("https://jaio.carrd.co/")
                    .AddField("pronouns", "meow/she/they", true)
                    .AddField("gender", "girl", true)
                    .AddField("sexuality", "pansexual", true)
                    .AddField("what are my aspirations", "in all honesty i would throw away most things to become someone's pet catgirl but other then that i want to make programs and get enough of a passive income to where all of my passions can stay fun and not turn into chores for me and i can live comfortably with my best friends and significant other or i could be a leftist politician that runs on a platform of human rights for every single person in the U.S.")
                    .WithColor(new DiscordColor("#FF1493"))
                    .WithImageUrl(images[c.Services.GetService<Random>().Next(1, 4)-1])));
                await c.Services.GetService<HttpClient>().DeleteAsync($"https://canary.discord.com/api/webhooks/{hook.Id}/{hook.Token}");
                return;
            }

            [Group("sample")]
            class cycliq_get_sample : BaseCommandModule
            {
                [Command("nekos.life"), RequireOwner, Description("Sample a nekos.life endpoint. **DEBUG COMMAND**")]
                public async Task Sample(CommandContext ctx, string endp)
                {
                    await ctx.TriggerTypingAsync();
                    DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
                    emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, endp));
                    await ctx.RespondAsync(embed: emb);

                }

            }




        }
        [Group("override"), Aliases("o")]
        class Override : BaseCommandModule
        {
            #pragma warning disable
            [Command("snipe"), Description("snipes messages deleted in the past 30 seconds"), RequireOwner]
            public async Task Snipe(CommandContext ctx)
            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
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
        [Command("removelast"), Aliases("rml")]
        [RequireOwner]
        public async Task RemoveLast(CommandContext ctx, int num = 1)
        {
            await ctx.TriggerTypingAsync();
            int deleted = 0;
            foreach (DiscordMessage msg in await ctx.Channel.GetMessagesAsync())
            {
                if (msg.Author.IsCurrent)
                {
                    msg.DeleteAsync();
                    deleted++;
                }
                if (!(deleted < num))
                    break;
            }
            ctx.RespondAsync($"Deleted past {deleted} messages that I sent in this channel (out of the last 100 messages).");
        }
        [Command("req"), RequireOwner]
        public async Task Request(CommandContext c, string m, [RemainingText]string url)
        {
            await c.TriggerTypingAsync();
            if (m == null || url == null)
            {
                await c.RespondAsync("you can't leave method or url blank!");
                return;
            };
            HttpClient cl = c.Services.GetService<HttpClient>();
            HttpResponseMessage res;
            switch (m.ToLower())
            {
                case "get":
                    res = await cl.GetAsync(url);
                    c.RespondAsync(new DiscordEmbedBuilder().WithDescription($@"
Request Type: GET
URL: {url}
Code: {res.StatusCode}
Content: {await res.Content.ReadAsStringAsync()}"));
                    break;
                case "delete":
                    res = await cl.DeleteAsync(url);
                    c.RespondAsync(new DiscordEmbedBuilder().WithDescription($@"
Request Type: DELETE
URL: {url}
Code: {res.StatusCode}
Content: {await res.Content.ReadAsStringAsync()}"));
                    break;
                default:
                    c.RespondAsync(m + " is not a valid method");
                    break;
            }
        }
    }
}
