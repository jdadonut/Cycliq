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
    class BasicCommands : BaseCommandModule
    {
        [Command("invite")]
        [Description("Gives you the bot's invite")]
        [Cooldown(1, 30, CooldownBucketType.User)]
        public async Task Invite(CommandContext ctx)
        {
            Console.WriteLine("[Cycliq] | Command \"Invite\" running...");
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync($"<https://discord.com/oauth2/authorize?client_id={ctx.Client.CurrentUser.Id}&scope=bot&permissions=8>");
        }
        [Command("eval")]
        [Description("Evaluate code")]
        [Cooldown(1, 30, CooldownBucketType.User)]
        public async Task Eval(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync("lol no");
        }
        [Command("bulk")]

        public async Task Bulk(CommandContext ctx, [RemainingText] string command)
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
                            ctx.CommandsNext.FindCommand(i, out commandargs),
                            commandargs
                        ));
            }
        }

    }
}