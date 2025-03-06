using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Chisel;

namespace Chisel.Modules.Generic
{
    public class GenericModule : ModuleBase<SocketCommandContext>
    {
        [Command("Ping")]
        [Summary("Echoes a ping.")]
        public async Task Ping() {
            await Context.Channel.SendMessageAsync("Hello World!");
        }
    }
}