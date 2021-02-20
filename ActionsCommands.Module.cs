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

        }
        [Group("holo")]
        [Aliases("hololive", "vtuber", "vtube", "vtb")]

        class ShowCommands_holo : BaseCommandModule
        {

            [Command("ero")]
            [Description("erotic holo image")]
            [RequireNsfw]
            [Aliases("erotic")]
            public async Task HoloERO(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();
                DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
                emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "holoero"));
                await ctx.RespondAsync(embed: emb);

            }

            [Command("sfw")]
            [Description("sfw holo image")]
            [GroupCommand]
            public async Task HoloSFW(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();
                DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
                emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "holo"));
                await ctx.RespondAsync(embed: emb);

            }
            [Command("nsfw")]
            [Description("nsfw holo image")]
            [Aliases("lewd", "ns", "porn")]
            [RequireNsfw]
            public async Task HoloNSFW(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();
                DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
                emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "hololewd"));
                await ctx.RespondAsync(embed: emb);

            }


        }
        [Group("yuri")]
        [Aliases("wlw", "2girls")]

        class ShowCommands_yurl : BaseCommandModule
        {


            [Command("ero")]
            [Description("erotic yuri image")]
            [Aliases("sfw")]
            [GroupCommand]
            public async Task YuriSFW(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();
                DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
                emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "eroyuri"));
                await ctx.RespondAsync(embed: emb);

            }
            [Command("nsfw")]
            [Description("nsfw yuri image")]
            [Aliases("lewd", "ns", "porn")]
            [RequireNsfw]
            public async Task YuriNSFW(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();
                DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
                emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "yuri"));
                await ctx.RespondAsync(embed: emb);

            }


        }

        // ! ONE-OFF COMMANDS BELOW

        [Command("futa")]
        [RequireNsfw]
        [Aliases("futanari")]
        [Description("nsfw futanari pictures")]
        public async Task Futa(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
            emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "futanari"));
            await ctx.RespondAsync(embed: emb);

        }
        [Command("smallboobs")]
        [RequireNsfw]
        [Aliases("flat", "smalltits")]
        [Description("nsfw flat pictures")]
        public async Task Flat(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
            emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "smallboobs"));
            await ctx.RespondAsync(embed: emb);

        }
        [Command("femboy")]
        [RequireNsfw]
        [Aliases("trap")]
        [Description("nsfw femboy pictures")]
        public async Task Femboy(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
            emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "trap"));
            await ctx.RespondAsync(embed: emb);

        }
        [Command("goose")]
        [Description("goose pictures")]
        public async Task Goose(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
            emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "goose"));
            await ctx.RespondAsync(embed: emb);

        }

        [Command("boobs")]
        [RequireNsfw]
        [Aliases("tits", "honkers")]
        [Description("nsfw boobs pictures")]
        public async Task Boobs(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
            emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "boobs"));
            await ctx.RespondAsync(embed: emb);

        }

        [Command("blowjob")]
        [RequireNsfw]
        [Description("nsfw blowjob pictures")]
        public async Task Blowjob(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
            emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, "blowjob"));
            await ctx.RespondAsync(embed: emb);

        }

    }
}

public class NekosLifeApiResponse : Newtonsoft.Json.Linq.JObject
{
    [JsonProperty("url")]
    public string url { get; set; }
}