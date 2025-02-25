using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.WebSocket;
using Chisel.Logs;
using Discord.Commands;

namespace Chisel.Startup
{
    public class StartupService
    {
        private static IServiceProvider _services;
        public static IServiceProvider CreateProvider()
        {
            var socketconfig = new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Info,
                MessageCacheSize = 150
            };

            var commandsconfig = new CommandServiceConfig() {
                CaseSensitiveCommands = false,
                LogLevel = LogSeverity.Debug
            };

            var collection = new ServiceCollection()

            // Socket
            .AddSingleton(socketconfig)
            .AddSingleton<DiscordSocketClient>()

            // Commands
            .AddSingleton(commandsconfig)
            .AddSingleton<CommandService>()

            // Logging
            .AddSingleton<LoggingService>(); 
            
            return collection.BuildServiceProvider();
        }
        public IServiceProvider GetServiceProvider()
        {
            _services = CreateProvider();
            return _services;
        }
    }
}

