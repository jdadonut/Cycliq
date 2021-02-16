using System;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
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

            if (__config.GetValue<string>("discord:token") == "")
            {
                throw new Exception("Token is equatable to nothing.");
            }
            Console.WriteLine("[INIT] Creating Discord Client");
            __discord = new DiscordClient(new DiscordConfiguration
            {
                Token = __config.GetValue<string>("discord:token"),
                TokenType = TokenType.Bot
            });

            __interactivity = __discord.UseInteractivity(new InteractivityConfiguration()
            {
                PaginationBehaviour = TimeoutBehaviour.Delete,
                PaginationTimeout = TimeSpan.FromSeconds(30),
                Timeout = TimeSpan.FromSeconds(30)
            });
            var deps = BuildDeps();
            __commands = __discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = __config.GetValue<string>("discord:CommandPrefix"),
                Dependencies = deps
            });
            var type = typeof(IModule);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);
            var typeList = types as Type[] ?? types.ToArray();
            foreach (var t in typeList)
                __commands.RegisterCommands(t);
            Console.WriteLine($"[Cycliq] Loaded {typeList.Count()} modules.");
            RunAsync(args).Wait();
        }
        async Task RunAsync(string[] args)
        {
            Console.WriteLine("[Cycliq] Connecting...");
            await __discord.ConnectAsync();
            Console.WriteLine("[Cycliq] Connected!");

            while (!__ctoken.IsCancellationRequested)
                await Task.Delay(TimeSpan.FromMinutes(1));
            
        }
        private DependencyCollection BuildDeps()
        {
            using var deps = new DependencyCollectionBuilder();
            deps.AddInstance(__interactivity)
                .AddInstance(__ctoken)
                .AddInstance(__config)
                .AddInstance<HttpClient>(new HttpClient())
                .AddInstance(__discord);
            return deps.Build();
        }
    }
}
