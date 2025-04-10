global using Discord;
using Discord.WebSocket;
using Chisel.Reader;
using Chisel.Services.Logs;
using Microsoft.Extensions.DependencyInjection;
using Chisel.Services.Startup;
using Discord.Commands;
using Chisel.Services.Handler;

// TODO MAKE A TEST EVENT WHEN A USER JOINS A GUILD.
// TODO TEST COMMAND SAYING "Hello World!"

// I have no idea how to use Depedency Injection sometimes so please be wary with this dumb console app

namespace Chisel
{
    public class InitClientStart
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly LoggingService _logsrv;
        private readonly CommandService _cmd;

        public string PrePrefix { get; set; }

        public InitClientStart(IServiceProvider provider)
        {
            _provider = provider;
            _client = provider.GetRequiredService<DiscordSocketClient>();
            _logsrv = provider.GetRequiredService<LoggingService>();
            _cmd = provider.GetRequiredService<CommandService>();
        }
        public static async Task Main() // screw static methods lmfao
        {
            var provider = StartupService.CreateProvider();
            var initializer = new InitClientStart(provider);
            await initializer.RunAsync();
            await Task.CompletedTask;
        }
        private async Task RunAsync()
        {
            var cmdservices = new CommandsHandler(_provider);
            var config = new JSONReader(); // Doesn't have to be there all the time

            await config.ReadJSON(".//Configurations//config.json");

            PrePrefix = config.CommandPrefix;

            _client.Log += _logsrv.LogAsync;
            _cmd.Log += _logsrv.LogAsync;

            await cmdservices.InstallModulesAsync();
            await _client.LoginAsync(TokenType.Bot, config.BotSecretToken);

            for (byte i = 3; i > 0;)
            {
                if (_client.ConnectionState != ConnectionState.Connected)
                {
                    await _client.StartAsync();
                    i--;
                }

                if (_client.ConnectionState == ConnectionState.Connected)
                {
                    break; // Checkmark
                }
            }

            await Task.Delay(-1); // Keep Task running to keep the bot running at all times (DON'T REMOVE THIS.)
        }
    }
}