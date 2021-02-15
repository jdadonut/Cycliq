using System;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.Configuration;

namespace Cycliq
{
    internal class Program
    {
        private CancellationTokenSource __ctoken { get; set; }
        private IConfigurationRoot      __config;
        private DiscordClient           __discord;
        private CommandsNextModule      __commands;
        private InteractivityModule     __interactivity;

        static async Task Main(string[] args) => await new Program().InitBot(args);
        async Task InitBot(string[] args)
        {
            Console.WriteLine("[INIT] Cycliq Bot Framework Loading...");
            __ctoken = new CancellationTokenSource();
            
            Console.WriteLine("[INIT] Loading configuration from JSON...");
            __config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: false, reloadOnChange: true)
                .Build();

            Console.WriteLine("[INIT] Creating Discord Client");
            __discord = new DiscordClient(new DiscordConfiguration
            {
                Token = __config.GetValue<string>("discord:token"),
                TokenType = TokenType.Bot
            });
            
        }
    }
}
