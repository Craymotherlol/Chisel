using Discord.WebSocket;
using Discord.Commands;
using Chisel;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Chisel.Services.Handler
{
    public class CommandsHandler
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        public CommandsHandler(IServiceProvider provider)
        {
            _provider = provider;
            _client = provider.GetRequiredService<DiscordSocketClient>();
            _commands = provider.GetRequiredService<CommandService>();
        }

        public async Task InstallModulesAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: _provider);
        }

        public async Task HandleCommandAsync(SocketMessage messageParam)
        {
            var getter = new InitClientStart(_provider);
            string prefix = getter.PrePrefix;

            if (prefix == null) {
                throw new ArgumentNullException();
            }

            if (messageParam == null) return;

            int argPos = 1;

            if (!(messageParam.HasStringPrefix(prefix, ref argPos) ||
                messageParam.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                messageParam.Author.IsBot)
                return;

            var context = new SocketCommandContext(_client, messageParam);

            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: _provider);
        }
    }
}