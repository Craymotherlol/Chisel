using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Chisel;

namespace Chisel.Commands.Generic
{
    class BotGenericCommands {

        static async Task Ping() {
            await Task.Delay(-1);
        }
    }
}