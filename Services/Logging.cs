using Discord;
using Discord.WebSocket;
using Chisel.reader;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Chisel.Startup;
using Discord.Commands;

// TODO MAKE A TEST EVENT WHEN A USER JOINS A GUILD.
// TODO TEST COMMAND SAYING "Hello World!"

// I have no idea how to use Depedency Injection 

namespace Chisel.Run
{
    class LoggingConsole
    {
        public Task LogAsync(LogMessage message)
        {

            if (message.Exception is CommandException commandException){

            }
                return Task.CompletedTask;
        }
    }
}