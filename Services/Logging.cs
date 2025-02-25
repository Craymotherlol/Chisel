using Discord;
using Discord.WebSocket;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Chisel.Startup;
using Discord.Commands;
using Discord.Net;

namespace Chisel.Logs
{
    class LoggingService
    {
        public Task LogAsync(LogMessage msg)
        {
            string SEVERITY = msg.Severity.ToString();
            switch (msg.Severity) // There's probably a better way to parse colorized logs into the console but eh this will do for now.
            {
                case LogSeverity.Critical:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.Write($"[{SEVERITY.ToUpper()}] ");
            Console.ResetColor();
            Console.WriteLine($"{msg.Message}");
            return Task.CompletedTask;
        }
    }
}