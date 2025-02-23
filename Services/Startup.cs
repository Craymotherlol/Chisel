using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.WebSocket;
using Chisel.Logs;


namespace Chisel.Startup
{
    public class StartupService
    {
        private static IServiceProvider _services;
        public static IServiceProvider CreateProvider()
        {
            var config = new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Info,
                MessageCacheSize = 150
            };

            var collection = new ServiceCollection()
            .AddSingleton(config)
            .AddSingleton<DiscordSocketClient>()
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

