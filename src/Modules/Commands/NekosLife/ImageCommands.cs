using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Cycliq.Agents;
namespace Cycliq.Commands.NekosLife
{
    [Category("Nekos.life", "Images"),
    Usage("{prefix}{command}"), Description("Get Nekos.Life images")]
    class ShowCommands : BaseCommandModule
    {
        [Group("feet")
        ]
        internal class ShowFeet : BaseCommandModule
        {
            [Command("ero"), 
            Description("erotic feet image"), 
            RequireNsfw, Aliases("erotic"), 
            GroupCommand, ]                  // [USES] nekos.life => feet
            public async Task FeetSFW(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "feet"); } 
            
            [Command("nsfw"), 
            Description("nsfw feet image"), 
            RequireNsfw,
            Category("Nekos.life", "Images"),
            Usage("{prefix}feet nsfw")]                                      // [USES] nekos.life => feetg
            public async Task FeetNSFW(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "feetg"); } 
        }
        [Group("holo"), Aliases("hololive", "vtuber", "vtube", "vtb")]
        internal class ShowHolo : BaseCommandModule
        {
            [Command("ero"), Description("erotic holo image"), RequireNsfw, Aliases("erotic")]                   // [USES] nekos.life => holoero
            public async Task HoloERO(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "holoero"); } 
            [Command("sfw"), Description("sfw holo image"), GroupCommand]                                        // [USES] nekos.life => holo
            public async Task HoloSFW(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "holo"); } 
            [Command("nsfw"), Description("nsfw holo image"), Aliases("lewd", "ns", "porn"), RequireNsfw]        // [USES] nekos.life => hololewd
            public async Task HoloNSFW(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "hololewd"); } 
        }
        [Group("yuri"), Aliases("wlw", "2girls")]
        internal class ShowYuri : BaseCommandModule
        {
            [Command("ero"), Description("erotic yuri image"), Aliases("sfw"), GroupCommand, RequireNsfw]                     // [USES] nekos.life => eroyuri
            public async Task YuriSFW(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "eroyuri"); }
            [Command("nsfw"), Description("nsfw yuri image"), Aliases("lewd", "ns", "porn"), RequireNsfw]        // [USES] nekos.life => yuri
            public async Task YuriNSFW(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "yuri"); }      
        }
        [Group("animalgirl"), Aliases("kemonomimi", "kemo")]
        internal class ShowKemonomimi : BaseCommandModule
        {
            [Command("ero"), Description("erotic kemonomimi image"), RequireNsfw, Aliases("erotic"), GroupCommand]             // [USES] nekos.life => erok, erokemo
            public async Task HoloERO(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "erok"); }
            [Command("nsfw"), Description("nsfw kemonomimi image"), Aliases("lewd", "ns", "porn"), RequireNsfw]  // [USES] nekos.life => lewdkemo, lewdk
            public async Task HoloNSFW(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "lewdkemo"); }
        }
        [Group("catgirl"), Aliases("neko")]
        internal class ShowNeko : BaseCommandModule
        {
            [Command("sfw"), Description("sfw catgirl images"), GroupCommand]                                    // [USES] nekos.life => neko
            public async Task NekoSFW(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "neko"); }
            [Command("nsfw"), Description("nsfw catgirl images"), RequireNsfw]                                   // [USES] nekos.life => nsfw_neko_gif
            public async Task NekoNSFW(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "nsfw_neko_gif"); }
        }

        // ! ONE-OFF COMMANDS BELOW
        [Command("futa"), RequireNsfw, Aliases("futanari"), Description("nsfw futanari pictures")]               // [USES] nekos.life => futanari
        public async Task Futa(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "futanari"); }
        
        [Command("smallboobs"), RequireNsfw, Aliases("flat", "smalltits"), Description("nsfw flat pictures")]    // [USES] nekos.life => smallboobs
        public async Task Flat(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "smallboobs"); }
        
        [Command("femboy"), RequireNsfw, Aliases("trap"), Description("nsfw femboy pictures")]                   // [USES] nekos.life => trap
        public async Task Femboy(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "trap"); }
        
        [Command("goose"), Description("goose pictures")]                                                        // [USES] nekos.life => goose
        public async Task Goose(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "goose"); }
       
        [Command("boobs"), RequireNsfw, Aliases("tits", "honkers"), Description("nsfw boobs pictures")]          // [USES] nekos.life => boobs
        public async Task Boobs(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "boobs"); }
        
        [Command("blowjob"), RequireNsfw, Description("nsfw blowjob pictures")]                                  // [USES] nekos.life => blowjob, bj
        public async Task Blowjob(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "bj"); }
        
        [Command("cat"), Description("meow!")]                                                                   // [USES] nekos.life => meow
        public async Task Meow(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "meow"); }
        
        [Command("lizard"), Description("lizard!")]                                                              // [USES] nekos.life => lizard
        public async Task Lizard(CommandContext ctx) { await NekosLifeAgent.DoNekosLifeCommand(ctx, "lizard"); }

    }
}