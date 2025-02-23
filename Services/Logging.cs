using Discord;
using Discord.WebSocket;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Chisel.Startup;
using Discord.Commands;

namespace Chisel.Logs
{
    class LoggingService
    {
        public Task LogAsync(LogMessage msg)    
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[INFO] ");
            Console.ResetColor();
            Console.WriteLine(msg);
            return Task.CompletedTask;
        }
    }
}