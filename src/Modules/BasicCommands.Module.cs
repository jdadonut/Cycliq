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
            await ctx.RespondAsync("i-");
            await ctx.TriggerTypingAsync();
            await Task.Delay(1000);
            await ctx.RespondAsync("you're kidding me, right?");
            await ctx.TriggerTypingAsync();
            await Task.Delay(1600);
            await ctx.RespondAsync("like did you actually think that would work?");
            await Task.Delay(1100);
            await ctx.RespondAsync("stupidest thing ive seen someone do all week");
        }
        [Command("bulk")]
        [RequireGuild]
        public async Task Bulk(CommandContext ctx, [RemainingText] string command)
        {
            if (command.Length == 0)
                return;
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