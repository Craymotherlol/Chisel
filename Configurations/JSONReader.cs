using Newtonsoft.Json;
using System;

namespace Chisel.reader {
    public class JSONReader // Handles JSON files to be read
    {
        public string BotSecretToken { get; set; }
        public string CommandPrefix { get; set; }

        public Task ReadJSON(string FILE) 
        {
            try
            {
                using (StreamReader sr = new StreamReader(FILE))
                {
                    string json = sr.ReadToEnd();
                    JSONBotConfiguration values = JsonConvert.DeserializeObject<JSONBotConfiguration>(json);
                    this.BotSecretToken = values.BotSecretToken;
                    this.CommandPrefix = values.CommandPrefix;
                }
            }catch
            {
                Console.WriteLine($"ERROR: Filepath is not set correctly: {FILE}");
            }
            return Task.CompletedTask;
        }
    }
    internal sealed class JSONBotConfiguration
    {
        public string BotSecretToken { get; set; }
        public string CommandPrefix { get; set; }
    }
}