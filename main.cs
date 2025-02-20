using Discord;
using Discord.WebSocket;
using Chisel.reader;
using Chisel.Run;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Chisel.Startup;
using Discord.Commands;

// TODO MAKE A TEST EVENT WHEN A USER JOINS A GUILD.
// TODO TEST COMMAND SAYING "Hello World!"

// I have no idea how to use Depedency Injection sometimes so please be wary with this dumb console app

namespace Chisel
{
    public class InitClientStart
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly JSONReader _jsonread;

        public InitClientStart(IServiceProvider provider)
        {
            _provider = provider;
            _client = provider.GetRequiredService<DiscordSocketClient>();
            _jsonread = provider.GetRequiredService<JSONReader>();
        }
        public static async Task Main()
        {
            var provider = StartupService.CreateProvider();
            var initializer = new InitClientStart(provider);
            await initializer.RunAsync(); // Invoke RunAsync
            await Task.CompletedTask;
        }
        public async Task RunAsync()
        {
            await _jsonread.ReadJSON(".//Configurations//config.json");
            try
            {
                await _client.LoginAsync(TokenType.Bot, _jsonread.BotSecretToken);
                await _client.StartAsync();
            }
            catch
            {
                Console.WriteLine("Token is either invalid or it is empty.");
            }

            await Task.Delay(-1); // Keep Task running to keep the bot running.
        }
    }
}