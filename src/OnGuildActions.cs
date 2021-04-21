using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable

namespace Cycliq
{
    class OnGuildActions
    {
        static string OwnerNotifyGuildJoin = @"
        **__Hey!__**
        I'm Cycliq, an Open-Source All-Purpose Discord Bot written in C#!
        If you're getting this message, that means that I was added to a server you own!
        Documentation can be found here: https://github.com/jdadonut/Cycliq
        Help can be found here: (eventual support server)
        Thank you for using Cycliq!"
            ;
        public static async Task OnGuildJoin_OwnerNotify(DiscordClient client, GuildCreateEventArgs args)
        {
            Task.Run(async () =>
            {
                (await args.Guild.Owner.CreateDmChannelAsync()).SendMessageAsync(OwnerNotifyGuildJoin);
            });
        }
    }
}
