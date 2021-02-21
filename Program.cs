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
            __config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: false, reloadOnChange: true)
                .Build();

            if (__config.GetValue<string>("discord:token") == "")
            {
                throw new Exception("Token is equatable to nothing.");
            }
            Console.WriteLine("[INIT]   | Creating Discord Client");
            __discord = new DiscordClient(new DiscordConfiguration
            {
                Token = __config.GetValue<string>("discord:token"),
                TokenType = TokenType.Bot
                
            });


            __interactivity = __discord.UseInteractivity();
            __voice = __discord.UseVoiceNext();
            __lavalink = __discord.UseLavalink();
            var deps = BuildDeps();
            __commands = __discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = new[] { __config.GetValue<string>("discord:CommandPrefix") },
                Services = BuildDeps(),
                EnableDms = false,

            });
            var type = typeof(BaseCommandModule);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract && !p.Namespace.StartsWith("DSharpPlus"));
            var typeList = types as Type[] ?? types.ToArray();
            foreach (var t in typeList)
            {
                Console.WriteLine($"[Cycliq] | Module {t.Name} Loaded...");
                __commands.RegisterCommands(t);
            }
            Console.WriteLine($"[Cycliq] | Loaded {typeList.Count()} modules.");
            RunAsync(args).Wait();
        }
        async Task RunAsync(string[] args)
        {
            Console.WriteLine("[Cycliq] | Connecting...");
            await __discord.ConnectAsync();
            Console.WriteLine("[Cycliq] | Connected!");
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
                .AddSingleton(__discord);
            return deps.BuildServiceProvider();
        }
    }
}
