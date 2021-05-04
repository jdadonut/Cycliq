using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Xml;
using Cycliq.Agents;

namespace Cycliq.Commands.Booru
{
    class Rule34 : BaseCommandModule
    {
        [Command("rule34"), Aliases("34", "r34"), RequireNsfw, GroupCommand]
            public async Task Rule34Command(CommandContext ctx, params string[] tags)
            {
            string ImageUrl = "";
            String ImageTags = "";
            string link = $"https://rule34.xxx/index.php?page=dapi&s=post&q=index&tags={String.Join("+", tags)}";
            HttpClient client = ctx.Services.GetService<HttpClient>();
            await ctx.TriggerTypingAsync();
            try
            {
                XmlDocument Res = new XmlDocument();
                Res.LoadXml(await (await client.GetAsync(link)).EnsureSuccessStatusCode().Content.ReadAsStringAsync());
                int count = int.Parse(Res.DocumentElement.SelectSingleNode("/posts").Attributes.GetNamedItem("count").Value);
                Random rand = ctx.Services.GetService<Random>();
                if (count == 0)
                    await ctx.RespondAsync("No posts found with those tags!");
                else
                {
                    BooruAgent.GetImageFromR34XMLDoc(out ImageUrl, out ImageTags, Res, rand);
                }
            }
            catch (Exception e)
            {
                await ctx.RespondAsync($"An \"{e.Message}\" error occurred.");
                return;
            }
            finally
            {
                DiscordEmbedBuilder em = new DiscordEmbedBuilder();
                if (ImageUrl != "")
                {
                    em.ImageUrl = ImageUrl;
                    em.Description = ImageTags;
                    em = em.WithFooter($"Search Results for {String.Join(" ", tags)}");
                    await ctx.RespondAsync(em);
                }
                else
                    await ctx.RespondAsync("No images found");


            }

        }

    }
}