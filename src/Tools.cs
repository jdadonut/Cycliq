using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using DSharpPlus.Interactivity.Extensions;
#pragma warning disable 1998

public static class Tools
{
    public static bool IsMention(string mention) { return new Regex("(<@|<@!)[0-9]{1,30}>").IsMatch(mention); }
    public static async Task<string> GetMention(CommandContext ctx, string mention, string message = "Please send a valid mention.")
    {
        await ctx.RespondAsync($"{mention} isn't a valid tag! {message}");
        InteractivityResult<DiscordMessage> mes = await GetMessageWithMention(ctx);
        if (mes.Result.Content.ToLower() == "cancel")
        {
            await ctx.RespondAsync("Command Cancelled.");
            return ctx.Message.Author.Id.ToString();
        }
        else
        {
            mention = mes.Result.Content;
            if (!IsMention(mention))
            {
                return await GetMention(ctx, mention, message);
            }
            else
                return mention;
        }


    }
    private static async Task<InteractivityResult<DiscordMessage>> GetMessageWithMention(CommandContext ctx)
    {
        return await ctx.Client.GetInteractivity().WaitForMessageAsync(
            c => c.Author.Id == ctx.Message.Author.Id,
            TimeSpan.FromSeconds(15));
    }
    public static DiscordColor GetColor(string hex){
        return new DiscordColor(hex);
    }

    public static class Voice
    {
        public static async Task<bool> IsUserInVoiceChannel(CommandContext ctx)
        {
            return !(ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null);
        }
    }
    public static class Video
    {
        public static async Task<bool> IsValid(string link)
        {
            return (Youtube.IsValid(link));
        }
        /*
        public static async Task<Stream>? Determine(string link)
        {
            if (Youtube.IsValid(link))
                return await Youtube.GetStream(link);
            else
                return null;
        }*/
        public static class Youtube
        {
            public static bool IsValid(string link)
            {
#pragma warning disable 1009
                Regex valid = new Regex(@"(?:https?:\/\/)?(?:www\.)?youtu(?:\.be\/|be.com\/\S*(?:watch|embed)(?:(?:(?=\/[^&\s\?]+(?!\S))\/)|(?:\S*v=|v\/)))([^&\s\?]+)");
                return valid.IsMatch(link);
            }
            /*
            public static async Task<Stream> GetStream(string link )

            {
               return new Stream()
            }

            public static GetVideoId
            */
        }
    }

    public static class Music {
        public static string DetermineAudioSource(CommandContext ctx, string video){
            if (video.Length == 0 && ctx.Message.Attachments.Count == 0) {
                return "none";
            }
            if (video.Length == 0){
                return "attachment";
            }
            if (Video.Youtube.IsValid(video)) {
                return "youtube-link";
            }
            return "none";


        }
    }

}
public static class Colors {
    static public string Youtube = "#FF0000";
}