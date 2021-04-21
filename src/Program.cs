using System;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.VoiceNext;
using DSharpPlus.Lavalink;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using MongoDB.Driver;
using Cycliq.Modules;
#pragma warning disable 1998

namespace Cycliq
{
    public class Program
    {
        public CancellationTokenSource __ctoken { get; set; }
        public IConfigurationRoot      __config;
        public DiscordClient           __discord;
        public CommandsNextExtension   __commands;
        public InteractivityExtension  __interactivity;
        public VoiceNextExtension      __voice;
        public LavalinkExtension       __lavalink;

        static async Task Main(string[] args) => await new Program().InitBot(args);
        async Task InitBot(string[] args)
        {
            Console.WriteLine("[INIT]   | Cycliq Bot Framework Loading...");
            __ctoken = new CancellationTokenSource();
            Console.WriteLine("[INIT]   | Loading configuration from JSON...");
            LoadConfiguration();
            CheckValidityOfToken();
            Console.WriteLine("[INIT]   | Creating Discord Client");
            BuildClient();
            BuildCommandNext();
            AddEvents();
            RunAsync(args).Wait();
        }

        private void CheckValidityOfToken()
        {
            if (__config.GetValue<string>("discord:token") == "")
            {
                throw new Exception("Token is equatable to nothing.");
            }
        }

        private void BuildCommandNext()
        {
            __commands = __discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = new[] { __config.GetValue<string>("discord:CommandPrefix"), "hey cycliq", "hey cycliq," },
                Services = BuildDeps(),
                //EnableDms = false,

            });
            LoadCommands();
        }

        private void LoadCommands()
        {
            var type = typeof(BaseCommandModule);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract && !p.Namespace.StartsWith("DSharpPlus") && !p.IsNested);
            var typeList = types as Type[] ?? types.ToArray();
            foreach (var t in typeList)
            {
                Console.WriteLine($"[Cycliq] | Module {t.Name} Loaded...");
                __commands.RegisterCommands(t);
            }
            Console.WriteLine($"[Cycliq] | Loaded {typeList.Count()} modules.");
        }

        private void BuildClient()
        {
            __discord = new DiscordClient(new DiscordConfiguration
            {
                Token = __config.GetValue<string>("discord:token"),
                TokenType = TokenType.Bot,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Error

            });


            __interactivity = __discord.UseInteractivity();
            __voice = __discord.UseVoiceNext();
            __lavalink = __discord.UseLavalink();
        }

        private void LoadConfiguration()
        {
            __config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: false, reloadOnChange: true)
                .Build();
        }

        private void AddEvents()
        {
            __discord.MessageDeleted += __discord.GetCommandsNext().Services.GetService<SnipeManager>().MessageDeleted; // SnipeManager
            __discord.GuildDownloadCompleted += (c, _) =>
            {
                Console.WriteLine($"[Cycliq] | Running in {__discord.Guilds.Count} Guilds!");
                return Task.CompletedTask;
            };
            __discord.GuildCreated += (_, __event) =>
            {
                Console.WriteLine($"[Cycliq] | Joined Guild \"{__event.Guild.Name}\" which has the ID {__event.Guild.Id} and is owned by \"{__event.Guild.Owner.Username}#{__event.Guild.Owner.Discriminator}\" (UID {__event.Guild.OwnerId})");

                return Task.CompletedTask;
            };
            __discord.GuildCreated += OnGuildActions.OnGuildJoin_OwnerNotify;

        }
        private async Task RunAsync(string[] args)
        {
            Console.WriteLine("[Cycliq] | Connecting...");
            await __discord.ConnectAsync();
            Console.WriteLine($"[Cycliq] | Connected");

            while (!__ctoken.IsCancellationRequested)
                await Task.Delay(TimeSpan.FromMinutes(1));
            
        }
        private ServiceProvider BuildDeps()
        {
            ServiceCollection deps = new ServiceCollection();
            deps.AddSingleton(__interactivity)
                .AddSingleton(__ctoken)
                .AddSingleton(__config)
                .AddSingleton<HttpClient>(new HttpClient())
                .AddSingleton<MongoClient>(new MongoClient(__config.GetValue<string>("mongo:url")))
                .AddSingleton(__voice)
                .AddSingleton<SnipeManager>(new SnipeManager())
                .AddSingleton<System.Random>(new System.Random())
                .AddSingleton<VoiceMangr>(new VoiceMangr())
                .AddSingleton(__discord);
            return deps.BuildServiceProvider();
        }
    }
}
