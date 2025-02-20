using Microsoft.Extensions.DependencyInjection;
using Chisel;
using Discord;
using Discord.WebSocket;
using Chisel.reader;


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
            .AddSingleton<JSONReader>();
            return collection.BuildServiceProvider();
        }
        public IServiceProvider GetServiceProvider()
        {
            _services = CreateProvider();
            return _services;
        }
    }
}

