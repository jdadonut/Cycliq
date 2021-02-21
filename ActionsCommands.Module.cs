using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using DSharpPlus;
namespace Cycliq
{
    [Group("do")]
    class ActionsCommands : BaseCommandModule
    {
        [Command("hug"), Description("Hug someone!")]                                                          // [USES] nekos.life => hug
        public async Task Hug(CommandContext ctx, string mention) { await Tools.DoActionCommand(ctx, "hug", "hug", "ed", mention); }
        [Command("kiss"), Description("Kiss someone!")]                                                        // [USES] nekos.life => kiss
        public async Task Kiss(CommandContext ctx, string mention) { await Tools.DoActionCommand(ctx, "kiss", "kiss", "es", mention); }
        [Command("cuddle"), Description("Cuddle someone!")]                                                    // [USES] nekos.life => cuddle
        public async Task Cuddle(CommandContext ctx, string mention) { await Tools.DoActionCommand(ctx, "cuddle", "cuddle", "ed", mention); }
        [Command("poke"), Description("Poke someone!")]                                                        // [USES] nekos.life => poke
        public async Task Poke(CommandContext ctx, string mention) { await Tools.DoActionCommand(ctx, "poke", "poke", "ed", mention); }
        [Command("tickle"), Description("Tickle someone!")]                                                        // [USES] nekos.life => poke
        public async Task Tickle(CommandContext ctx, string mention) { await Tools.DoActionCommand(ctx, "tickle", "tickl", "ed", mention); }
        [Command("feed"), Description("Feed someone!")]                                                        // [USES] nekos.life => poke
        public async Task Feed(CommandContext ctx, string mention) { await Tools.DoActionCommand(ctx, "feed", "fed", "", mention); }
        [Command("baka"), Description("baka!")]                                                        // [USES] nekos.life => poke
        public async Task Baka(CommandContext ctx) { await Tools.DoSelfActionCommand(ctx, "baka", ": \"B-Baka...\""); }

    }
    [Group("show")]
    class ShowCommands : BaseCommandModule
    {
        [Group("feet")]
        class ShowCommands_feet : BaseCommandModule
        {
            [Command("ero"), Description("erotic feet image"), RequireNsfw, Aliases("erotic")]                  // [USES] nekos.life => feet
            public async Task FeetSFW(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "feet"); } 
            [Command("sfw"), Description("sfw feet image"), GroupCommand]                                       // [USES] nekos.life => erofeet
            public async Task FeetERO(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "erofeet"); } 
            [Command("nsfw"), Description("nsfw feet image"), RequireNsfw]                                      // [USES] nekos.life => feetg
            public async Task FeetNSFW(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "feetg"); } 
        }
        [Group("holo"), Aliases("hololive", "vtuber", "vtube", "vtb")]
        class ShowCommands_holo : BaseCommandModule
        {
            [Command("ero"), Description("erotic holo image"), RequireNsfw, Aliases("erotic")]                   // [USES] nekos.life => holoero
            public async Task HoloERO(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "holoero"); } 
            [Command("sfw"), Description("sfw holo image"), GroupCommand]                                        // [USES] nekos.life => holo
            public async Task HoloSFW(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "holo"); } 
            [Command("nsfw"), Description("nsfw holo image"), Aliases("lewd", "ns", "porn"), RequireNsfw]        // [USES] nekos.life => hololewd
            public async Task HoloNSFW(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "hololewd"); } 
        }
        [Group("yuri"), Aliases("wlw", "2girls")]
        class ShowCommands_yuri : BaseCommandModule
        {
            [Command("ero"), Description("erotic yuri image"), Aliases("sfw"), GroupCommand]                     // [USES] nekos.life => eroyuri
            public async Task YuriSFW(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "eroyuri"); }
            [Command("nsfw"), Description("nsfw yuri image"), Aliases("lewd", "ns", "porn"), RequireNsfw]        // [USES] nekos.life => yuri
            public async Task YuriNSFW(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "yuri"); }      
        }
        [Group("animalgirl"), Aliases("kemonomimi", "kemo")]
        class ShowCommands_kemonomimi : BaseCommandModule
        {
            [Command("ero"), Description("erotic kemonomimi image"), RequireNsfw, Aliases("erotic")]             // [USES] nekos.life => erok, erokemo
            public async Task HoloERO(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "erok"); }
            [Command("sfw"), Description("sfw kemonomimi image"), GroupCommand]                                  // [USES] nekos.life => kemo, kemonomimi
            public async Task HoloSFW(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "kemonomimi"); }
            [Command("nsfw"), Description("nsfw kemonomimi image"), Aliases("lewd", "ns", "porn"), RequireNsfw]  // [USES] nekos.life => lewdkemo, lewdk
            public async Task HoloNSFW(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "lewdkemo"); }
        }
        [Group("catgirl"), Aliases("neko")]
        class ShowCommands_neko : BaseCommandModule
        {
            [Command("sfw"), Description("sfw catgirl images"), GroupCommand]                                     // [USES] nekos.life => neko
            public async Task NekoSFW(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "neko"); }
            [Command("nsfw"), Description("nsfw catgirl images"), RequireNsfw]                                    // [USES] nekos.life => nsfw_neko_gif
            public async Task NekoNSFW(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "nsfw_neko_gif"); }
        }
        // ! ONE-OFF COMMANDS BELOW
        [Command("futa"), RequireNsfw, Aliases("futanari"), Description("nsfw futanari pictures")]               // [USES] nekos.life => futanari
        public async Task Futa(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "futanari"); }
        [Command("smallboobs"), RequireNsfw, Aliases("flat", "smalltits"), Description("nsfw flat pictures")]    // [USES] nekos.life => smallboobs
        public async Task Flat(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "smallboobs"); }
        [Command("femboy"), RequireNsfw, Aliases("trap"), Description("nsfw femboy pictures")]                   // [USES] nekos.life => trap
        public async Task Femboy(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "trap"); }
        [Command("goose"), Description("goose pictures")]                                                        // [USES] nekos.life => goose
        public async Task Goose(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "goose"); }
        [Command("boobs"), RequireNsfw, Aliases("tits", "honkers"), Description("nsfw boobs pictures")]          // [USES] nekos.life => boobs
        public async Task Boobs(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "boobs"); }
        [Command("blowjob"), RequireNsfw, Description("nsfw blowjob pictures")]                                  // [USES] nekos.life => blowjob, bj
        public async Task Blowjob(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "bj"); }
        [Command("cat"), Description("meow!")]                                                                  // [USES] nekos.life => meow
        public async Task Meow(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "meow"); }
        [Command("lizard"), Description("lizard!")]                                                             // [USES] nekos.life => lizard
        public async Task Lizard(CommandContext ctx) { await Tools.DoNekosLifeCommand(ctx, "lizard"); }

    }
}

public class NekosLifeApiResponse : Newtonsoft.Json.Linq.JObject
{
    [JsonProperty("url")]
    public string url { get; set; }
}