using Discord;
using Discord.WebSocket;
using Chisel.Reader;
using Chisel.Logs;
using System;
using Microsoft.Extensions.DependencyInjection;
using Chisel.Startup;
using Discord.Commands;
using Chisel.Modules;
using Discord.Rest;

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
            Console.WriteLine($"State: {_client.LoginState}");
            var cmdservices = new CommandsServicesHandler(_provider);
            var config = new JSONReader(); // Doesn't have to be there all the time

            await config.ReadJSON(".//Configurations//config.json");
            _client.Log += _logsrv.LogAsync;
            _cmd.Log += _logsrv.LogAsync;

            PrePrefix = config.CommandPrefix; // Grab from JSON to pass it to CommandServicesHandler class.

            await _client.LoginAsync(TokenType.Bot, config.BotSecretToken);

            for (byte i = 3; i > 0;)
            {
                await _client.StartAsync();
                Console.WriteLine($"{i} Tries left.");
                i--;

                if (_client.ConnectionState == ConnectionState.Connected)
                {
                    break;
                }
            }

            await Task.Delay(-1); // Keep Task running to keep the bot running at all times (DON'T REMOVE THIS.)
        }
    }
}