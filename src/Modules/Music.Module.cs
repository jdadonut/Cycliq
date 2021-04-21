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
using Cycliq.Modules;

namespace Cycliq
{
    [Group("music")]
    class Music
    {
        [Command("play"), Description("play music!"), RequireUserPermissions(Permissions.UseVoice), RequireBotPermissions(Permissions.UseVoice)]
        public async Task Play(CommandContext ctx, string video)
        {
            
        }
        [Command("join"), Description("join voice chat!"), RequireUserPermissions(Permissions.UseVoice), RequireBotPermissions(Permissions.UseVoice)]

        public async Task Join(CommandContext ctx)
        {
            
        }
        internal class internals {

        }
    }
    
}
