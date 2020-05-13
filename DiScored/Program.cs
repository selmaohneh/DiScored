using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace DiScored
{
    public class Program
    {
        private DiscordSocketClient _client;

        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please provide the bot token as only argument!");
                return;
            }

            new Program().MainAsync(args[0]).GetAwaiter().GetResult();
        }

        public async Task MainAsync(string token)
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;

            // Remember to keep token private or to read it from an 
            // external source! In this case, we are reading the token 
            // from an environment variable. If you do not know how to set-up
            // environment variables, you may find more information on the 
            // Internet or by using other methods such as reading from 
            // a configuration.
            await _client.LoginAsync(TokenType.Bot,
                token);
            await _client.StartAsync();

            var commandService = new CommandService();
            var commandHandler = new CommandHandler(_client, commandService);

            await commandHandler.InstallCommandsAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}