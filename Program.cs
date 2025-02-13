using DSharpPlus;
using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace Chisel
{
    public class MainRuntime
    {
        public static async Task Main(string[] args)
        {
            JSONReader reader = new JSONReader();
            await reader.ReadJSON(".//cogs//config.json");
            var CLIENT = new DiscordClient(new DiscordConfiguration
            {
                Token = reader.BotSecretToken,
                MinimumLogLevel = LogLevel.Debug,
            });

            await CLIENT.ConnectAsync();

            await Task.Delay(-1);
        } 
    }
    public class JSONReader // Handles JSON files to be read
    {
        public string BotSecretToken { get; set; }
        public string CommandPrefix { get; set; }

        public async Task ReadJSON(string FILE) 
        {
            try
            {
                using (StreamReader sr = new StreamReader(FILE))
                {
                    string json = await sr.ReadToEndAsync();
                    JSONBotConfiguration values = JsonConvert.DeserializeObject<JSONBotConfiguration>(json);
                    BotSecretToken = values.BotSecretToken;
                    Console.WriteLine($"token:{BotSecretToken}");
                    CommandPrefix = values.CommandPrefix;
                }
            }catch
            {
                Console.WriteLine($"ERROR: Filepath is not set correctly: {FILE}");
            }
        }
    }
    internal sealed class JSONBotConfiguration
    {
        public string BotSecretToken { get; set; }
        public string CommandPrefix { get; set; }
    }
}