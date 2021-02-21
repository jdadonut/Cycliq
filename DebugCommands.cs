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
    [Group("cycliq")]
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

        [Group("get")]
        class Cycliq_get : BaseCommandModule
        {
            [Command("token")]
            [Description("Get the current bot token!")]
            public async Task getToken(CommandContext ctx, [RemainingText] string s)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync("wh-");
                await ctx.TriggerTypingAsync();
                await Task.Delay(1000);
                await ctx.RespondAsync("no?");
            }
            [Group("sample")]
            class cycliq_get_sample : BaseCommandModule
            {
                [Command("nekos.life")]
                [RequireOwner]
                [Description("Sample a nekos.life endpoint. **DEBUG COMMAND**")]
                public async Task Sample(CommandContext ctx, string endp)
                {
                    await ctx.TriggerTypingAsync();
                    DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
                    emb.WithImageUrl(await Tools.GetNekosLifeEndpoint(ctx, endp));
                    await ctx.RespondAsync(embed: emb);

                }

            }




        }

        [Command("bulk")]
        public async Task Bulk(CommandContext ctx, [RemainingText]string command)
        {
            string[] commands = command.Split("\n");
            if (commands.Length > 50)
                Array.Resize(ref commands, 50);
            foreach (var i in commands)
            {
                string commandargs;
                if (i != null && i.Trim() != "" && !i.StartsWith("bulk"))
                    await ctx.CommandsNext.ExecuteCommandAsync(
                        ctx.CommandsNext.CreateFakeContext(
                            ctx.Member,
                            ctx.Channel,
                            i,
                            ctx.Prefix,
                            ctx.CommandsNext.FindCommand(i,out commandargs),
                            commandargs
                        ));
            }
        }
    }
    
    
}
