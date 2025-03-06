using Discord.WebSocket;
using System;
using System.Text;
using System.Collections.Generic;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Chisel.Modules
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

        public async Task CommandAssemblyInstaller()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _cmd.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: _provider);
        }
        public async Task HandleCommandAsync(SocketMessage message_parameter)
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