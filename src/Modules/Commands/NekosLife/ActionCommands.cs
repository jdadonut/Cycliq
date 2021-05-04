using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using Cycliq.Agents;

namespace Cycliq.Commands.NekosLife
{
    class ActionCommands : BaseCommandModule
    {
        [Command("hug"), 
        Description("Hug someone!"), 
        Category("Nekos.life", "Action"),
        Usage("{prefix}hug {Mention}")]                                                       // [USES] nekos.life => hug
        public async Task Hug(CommandContext ctx, string mention) { await NekosLifeAgent.DoActionCommand(ctx, "hug", "hug", "ged", mention); }
        [Command("kiss"), 
        Description("Kiss someone!"), 
        Category("Nekos.life", "Action"),
        Usage("{prefix}kiss {mention}")]                                                      // [USES] nekos.life => kiss
        public async Task Kiss(CommandContext ctx, string mention) { await NekosLifeAgent.DoActionCommand(ctx, "kiss", "kiss", "ed", mention); }
        [Command("cuddle"), 
        Description("Cuddle someone!"), 
        Category("Nekos.life", "Action"),
        Usage("{prefix}cuddle {Mention}")]                                                    // [USES] nekos.life => cuddle
        public async Task Cuddle(CommandContext ctx, string mention) { await NekosLifeAgent.DoActionCommand(ctx, "cuddle", "cuddle", "d", mention); }
        [Command("poke"), 
        Description("Poke someone!"), 
        Category("Nekos.life", "Action"),
        Usage("{prefix}poke {Mention}")]                                                      // [USES] nekos.life => poke
        public async Task Poke(CommandContext ctx, string mention) { await NekosLifeAgent.DoActionCommand(ctx, "poke", "poke", "ed", mention); }
        [Command("tickle"), 
        Description("Tickle someone!"), 
        Category("Nekos.life", "Action"),
        Usage("{prefix}tickle {Mention}")]                                                    // [USES] nekos.life => tickle
        public async Task Tickle(CommandContext ctx, string mention) { await NekosLifeAgent.DoActionCommand(ctx, "tickle", "tickl", "ed", mention); }
        [Command("feed"), 
        Description("Feed someone!"), 
        Category("Nekos.life", "Action"),
        Usage("{prefix}feed {Mention}")]                                                      // [USES] nekos.life => feed
        public async Task Feed(CommandContext ctx, string mention) { await NekosLifeAgent.DoActionCommand(ctx, "feed", "fed", "", mention); }
        [Command("baka"), 
        Description("baka!"), 
        Category("Nekos.life", "Action"),
        Usage("{prefix}baka")]                                                                // [USES] nekos.life => poke
        public async Task Baka(CommandContext ctx) { await NekosLifeAgent.DoSelfActionCommand(ctx, "baka", ": \"B-Baka...\""); }
    }

}