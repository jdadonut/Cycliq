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
    class ActionsCommands : IModule
    {
        string NekosLifeApiPoint = "https://nekos.life/api/v2/img";

        [Command("hug")]
        [Description("Hug someone!")]
        public async Task Hug(CommandContext ctx, string mention)
        {
            Console.WriteLine("[Cycliq] Command \"Hug\" running...");
            await ctx.TriggerTypingAsync();
            while (true)
            {
                if (!new Regex("(<@|<@!)[0-9]{1,30}>").IsMatch(mention))
                {
                    await ctx.RespondAsync($"{mention} isn't a valid tag! Send a valid tag to hug that person!");
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

            try
            {
                var Embed = new DiscordEmbedBuilder();
                Embed.ImageUrl = ((Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(
                    (await
                        (await
                            ctx.Dependencies.GetDependency<HttpClient>()
                                .GetAsync(NekosLifeApiPoint + "/hug"))
                                    .Content
                                        .ReadAsStringAsync()
                            )
                        )
                    ).Value<string>("url");
                Embed.Description = $"<@{ctx.Message.Author.Id}> hugged {mention}";
                await ctx.RespondAsync(embed: Embed);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                await ctx.RespondAsync("Command errored out, error logged.");
            }

            return;

        }
        [Command("kiss")]
        [Description("Kiss someone!")]
        public async Task Kiss(CommandContext ctx, string mention)
        {
            Console.WriteLine("[Cycliq] Command \"Kiss\" running...");
            await ctx.TriggerTypingAsync();
            while (true)
            {
                if (!new Regex("(<@|<@!)[0-9]{1,30}>").IsMatch(mention))
                {
                    await ctx.RespondAsync($"{mention} isn't a valid tag! Send a valid tag to kiss that person!");
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

            try
            {
                var Embed = new DiscordEmbedBuilder();
                Embed.ImageUrl = ((Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject((await (await ctx.Dependencies.GetDependency<HttpClient>().GetAsync(NekosLifeApiPoint + "/kiss")).Content.ReadAsStringAsync()))).Value<string>("url");
                Embed.Description = $"<@{ctx.Message.Author.Id}> kissed {mention}";
                await ctx.RespondAsync(embed: Embed);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                await ctx.RespondAsync("Command errored out, error logged.");
            }

            return;

        }
    }
}

public class NekosLifeApiResponse : Newtonsoft.Json.Linq.JObject
{
    public string url { get; set; }
}