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
    [Group("do")]
    class ActionsCommands : BaseCommandModule
    {



        [Command("hug")]
        [Description("Hug someone!")]
        public async Task Hug(CommandContext ctx, string mention)
        {
            await ctx.TriggerTypingAsync();
            if (!Tools.IsMention(mention))
                mention = await Tools.GetMention(ctx, mention, "Please mention the user you want to hug!");
            if (mention == ctx.Message.Author.Id.ToString())
                return;
            DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
            emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "hug"))
                .WithDescription($"<@!{ctx.Message.Author.Id}> hugged {mention}");
            await ctx.RespondAsync(embed: emb);

        }
        [Command("kiss")]
        [Description("Kiss someone!")]
        public async Task Kiss(CommandContext ctx, string mention)
        {
            await ctx.TriggerTypingAsync();
            if (!Tools.IsMention(mention))
                mention = await Tools.GetMention(ctx, mention, "Please mention the user you want to kiss!");
            if (mention == ctx.Message.Author.Id.ToString())
                return;
            DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
            emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "kiss"))
                .WithDescription($"<@!{ctx.Message.Author.Id}> kissed {mention}");
            await ctx.RespondAsync(embed: emb);


        }
        [Command("cuddle")]
        [Description("Cuddle someone!")]
        public async Task Cuddle(CommandContext ctx, string mention)
        {
            await ctx.TriggerTypingAsync();
            if (!Tools.IsMention(mention))
                mention = await Tools.GetMention(ctx, mention, "Please mention the user you want to cuddle!");
            if (mention == ctx.Message.Author.Id.ToString())
                return;
            DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
            emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "cuddle"))
                .WithDescription($"<@!{ctx.Message.Author.Id}> is cuddling with {mention}");
            await ctx.RespondAsync(embed: emb);


        }
        [Command("poke")]
        [Description("Poke someone!")]
        public async Task Poke(CommandContext ctx, string mention)
        {
            await ctx.TriggerTypingAsync();
            if (!Tools.IsMention(mention))
                mention = await Tools.GetMention(ctx, mention, "Please mention the user you want to poke!");
            if (mention == ctx.Message.Author.Id.ToString())
                return;
            DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
            emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "poke"))
                .WithDescription($"<@!{ctx.Message.Author.Id}> poked {mention}");
            await ctx.RespondAsync(embed: emb);


        }

    }
    [Group("show")]
    class ShowCommands : BaseCommandModule
    {
        [Group("feet")]
        
        class ShowCommands_feet : BaseCommandModule
        {

            [Command("ero")]
            [Description("erotic feet image")]
            [RequireNsfw]
            [Aliases("erotic", "nsfw")]
            public async Task FeetSFW(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();
                DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
                emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "feet"));
                await ctx.RespondAsync(embed: emb);

            }

            [Command("sfw")]
            [Description("sfw feet image")]
            [GroupCommand]
            public async Task FeetERO(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();
                DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
                emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "erofeet"));
                await ctx.RespondAsync(embed: emb);

            }
            public async Task Feet(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();
                DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
                emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "feet"));
                await ctx.RespondAsync(embed: emb);

            }
        }
    }
}

public class NekosLifeApiResponse : Newtonsoft.Json.Linq.JObject
{
    [JsonProperty("url")]
    public string url { get; set; }
}