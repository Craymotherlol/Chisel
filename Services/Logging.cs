using Discord;
using Discord.WebSocket;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Chisel.Startup;
using Discord.Commands;

namespace Chisel.Run
{
    class LoggingService
    {

        public LoggingService(DiscordSocketClient _client)
        {
            _client.Log += LogAsync;
        }
        private Task LogAsync(LogMessage msg)
        {
                return Task.CompletedTask;
        }
    }
}