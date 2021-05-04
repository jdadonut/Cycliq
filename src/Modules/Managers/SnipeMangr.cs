using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#pragma warning disable 
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
            Task.Run(async () =>
            {
                await ManageDeletedMessage(sender, e);
            });
        }

        private async Task ManageDeletedMessage(DiscordClient sender, MessageDeleteEventArgs e)
        {
            Dictionary<ulong, DiscordMessage>? t = null;
            this.TryGetValue(e.Guild.Id, out t);
            if (t == null)
            {
                await SnipeWhereNull(sender, e);
            }
            t.TryAdd(e.Message.Id, e.Message);
            await Task.Delay(TimeSpan.FromSeconds(30));
            DiscordMessage? m;
            this.TryGetValue(e.Guild.Id, out t);
            if (t != null)
            {
                m = RemoveMessageFromStorage(e, t);
            }
        }

        private static DiscordMessage RemoveMessageFromStorage(MessageDeleteEventArgs e, Dictionary<ulong, DiscordMessage> t)
        {
            DiscordMessage m;
            t.TryGetValue(e.Message.Id, out m);
            if (m != null)
            {
                t.Remove(e.Message.Id);

            }

            return m;
        }

        private async Task SnipeWhereNull(DiscordClient sender, MessageDeleteEventArgs e)
        {
            this.TryAdd(e.Guild.Id, new Dictionary<ulong, DiscordMessage> { });
            await MessageDeleted(sender, e);
            return;
        }
    }
}
