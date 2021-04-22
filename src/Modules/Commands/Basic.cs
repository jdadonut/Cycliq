using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;


namespace Cycliq.Commands
{
    public class Basic : BaseCommandModule
    {
        [Command("invite"),
        Description("Gives you the bot's invite"),
        Cooldown(1, 30, CooldownBucketType.User),
        Usage("{prefix}invite"),
        Category("Miscellanious")]
        public async Task Invite(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync($"<https://discord.com/oauth2/authorize?client_id={ctx.Client.CurrentUser.Id}&scope=bot&permissions=8>");
        }
        [Command("intro"), RequireOwner]
            public async Task getIntro(CommandContext c, DiscordChannel ch = null)
            {
                ch = ch ?? c.Channel;
                DiscordWebhook hook = null;
                await c.TriggerTypingAsync();
                hook = await ch.CreateWebhookAsync("[Cycliq]", reason: "Command run: `get intro`");
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
    }
}