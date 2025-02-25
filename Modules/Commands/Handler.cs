using Chisel;
using Chisel.Reader;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Net.NetworkInformation;

namespace Chisel.Commands
{
    public class CommandsServicesHandler
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _cmd;

    
        public CommandsServicesHandler(IServiceProvider provider)
        {
            _provider = provider;
            _client = provider.GetRequiredService<DiscordSocketClient>();
            _cmd = provider.GetRequiredService<CommandService>();
        }
        private async Task HandleCommandAsync(SocketMessage message_parameter)
        { 
            var getter = new InitClientStart(_provider);
            string prefix = getter.PrePrefix;

            var message = message_parameter as SocketUserMessage;
            if (message == null) return;

            int argPos = 1;

            if (!(message.HasStringPrefix(prefix, ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            var context = new SocketCommandContext(_client, message);

            await _cmd.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);
        }
    }
}