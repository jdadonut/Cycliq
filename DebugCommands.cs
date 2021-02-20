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

        }


    }
    
}
