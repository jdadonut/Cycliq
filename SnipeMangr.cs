using DSharpPlus;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cycliq
{
    public class SnipeManager : Dictionary<ulong, Dictionary<ulong, DiscordMessage>>
    {

        public Dictionary<ulong, DiscordMessage>? FindMessagesByServer(ulong id, bool del = true)
        {
            Dictionary<ulong, DiscordMessage>? list;
            this.TryGetValue(id, out list);
            if (del)
                this.Remove(id);
            return list;
        }
        public async Task MessageDeleted(DiscordClient sender, DSharpPlus.EventArgs.MessageDeleteEventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(async () =>
            {
                Dictionary<ulong, DiscordMessage>? t = null;
                this.TryGetValue(e.Guild.Id, out t);
                if (t == null)
                {
                    this.TryAdd(e.Guild.Id, new Dictionary<ulong, DSharpPlus.Entities.DiscordMessage> { });
                    await MessageDeleted(sender, e);
                    return;
                }
                t.TryAdd(e.Message.Id, e.Message);
                await Task.Delay(TimeSpan.FromSeconds(30));
                DiscordMessage? m;
                this.TryGetValue(e.Guild.Id, out t);
                if (t != null)
                {
                    t.TryGetValue(e.Message.Id, out m);
                    if (m != null)
                    {
                        t.Remove(e.Message.Id);

                    }
                }
            });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed



        }

    }
}
