using Microsoft.Extensions.DependencyInjection;
using Chisel;
using Discord;
using Discord.WebSocket;


namespace Chisel.Startup
{
    public class StartupService
    {
        private static IServiceProvider _services;
        private static IServiceProvider CreateProvider()
        {
            var config = new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Info,
                MessageCacheSize = 150
            };

            var collection = new ServiceCollection()
            .AddSingleton(config)
            .AddSingleton<DiscordSocketClient>();
            return collection.BuildServiceProvider();
        }
        public static IServiceProvider GetServiceProvider()
        {
            _services = CreateProvider();
            return _services;
        }

    }
}

