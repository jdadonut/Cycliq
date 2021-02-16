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
    class BasicCommandsModule : IModule
    {
        [Command("invite")]
        [Description("Gives you the bot's invite")]
        [Cooldown(1, 30, CooldownBucketType.User)]
        public async Task Invite(CommandContext ctx)
        {
            Console.WriteLine("[Cycliq] Command \"Invite\" running...");
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync("<https://discord.com/oauth2/authorize?client_id=810337342283579422&scope=bot&permissions=8>");
        }

    }
}