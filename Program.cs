using Discord.WebSocket;
using Discord;
using System.IO;
using Newtonsoft.Json;

namespace Chisel
{
    public class MainRuntime
    {
        private DiscordSocketClient _client; // Discord's API

        public static async Task Main()
        {
            JSONReader reader = new JSONReader();

            await reader.ReadJSON(".//cogs//config.json");
            await Task.Delay(-1);
        } 
    }
    public class JSONReader // Handles JSON files to be read
    {
        public async Task ReadJSON(string FILE) 
        {
            string filePath = Path.GetFullPath(FILE);
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string json = await sr.ReadToEndAsync();
                    dynamic values = JsonConvert.DeserializeObject<dynamic>(json);
                    foreach (var cog in values)
                    {
                        Console.WriteLine(cog.ToString());
                    }
                }
            }catch
            {
                Console.WriteLine($"ERROR: Filepath is not set correctly: {filePath}");
            }
        }
    }
}