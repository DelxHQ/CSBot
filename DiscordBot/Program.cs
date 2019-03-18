using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace DiscordBot
{
    class Program
    {
        static void Main(string[] args) 
            => new Program().Start(args).GetAwaiter().GetResult();

        public static DiscordSocketClient _client;
        public static Program _instance = new Program();

        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

        public async Task Start(string[] args)
        {
            await Login();
            await Task.Delay(-1);
      
        }

        public async Task Login()
        {
            _instance = this;
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,
                MessageCacheSize = 50,
                AlwaysDownloadUsers = true
            });

            _client.Log += Log;
            _client.MessageReceived += MessageReceived;

            await _client.LoginAsync(TokenType.Bot, "");
            await _client.StartAsync();
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Content == "+ping")
            {
                await message.Channel.SendMessageAsync("I'm alive!");
            }
        }
    }
}
