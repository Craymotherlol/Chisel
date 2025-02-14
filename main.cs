using Discord;
using Discord.WebSocket;
using Chisel.reader;
using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

// TODO MAKE A TEST EVENT WHEN A USER JOINS A GUILD.
// TODO TEST COMMAND SAYING "Hello World!"

namespace Chisel
{
    public class MainRuntime
    {
        public static DiscordSocketClient _CLIENT_; // Aka bot it logs into
        public static async Task Main(string[] args)
        {
            JSONReader reader = new JSONReader();
            await reader.ReadJSON(".//cogs//config.json");

            try {
                await _CLIENT_.LoginAsync(TokenType.Bot, reader.BotSecretToken);
                await _CLIENT_.StartAsync();
            }catch {
                Console.WriteLine("Token is either invalid or it is empty.");
            }

            await Task.Delay(-1);
        } 
    }
}