using System;
using System.Collections.Generic;
using System.Text;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.VoiceNext;
using DSharpPlus.VoiceNext.Codec;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
namespace Cycliq
{
    [Group("music")]
    class Music
    {
        [Command("play"), Description("play music!"), RequireUserPermissions(Permissions.UseVoice), RequireBotPermissions(Permissions.UseVoice)]
        public async Task Play(CommandContext ctx, string video)
        {
            if (await Join(ctx))
            {
                if (!await Tools.Video.IsValid(video))
                    return;
                VoiceNextConnection con = await ctx.Member.VoiceState.Channel.ConnectAsync();
                VoiceTransmitSink sink = con.GetTransmitSink();
            }
        }
        [Command("join"), Description("join voice chat!"), RequireUserPermissions(Permissions.UseVoice), RequireBotPermissions(Permissions.UseVoice)]

        public async Task<bool> Join(CommandContext ctx)
        {
            if (!await Tools.Voice.IsUserInVoiceChannel(ctx))
            {
                await ctx.RespondAsync("You need to be in a voice channel for that!");
                return false;
            }
            return false;
            
        }
    }
}
