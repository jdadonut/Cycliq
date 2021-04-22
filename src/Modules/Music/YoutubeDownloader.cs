using System.IO;
using System.Threading.Tasks;
using VideoLibrary;
using NAudio;
using DSharpPlus.CommandsNext;
namespace Cycliq.Music.Youtube {
    class YoutubeMusic {
        async public static Task<MemoryStream> GetStreamFromYoutubeURL(string url)
        {
            return new MemoryStream();
        }
        async public static Task<object> GetYoutubeVideoData(string url)
        {
            return "";
        }
    }
}
