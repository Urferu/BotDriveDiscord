using System;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace DriveBot
{
    class Program
    {
        const string _TOCKEN = "";
        private DiscordSocketClient client;
        private CommandService commands;
        static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });
            commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            client.MessageReceived += Client_MessageReceived;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());

            client.Ready += Client_Ready;
            client.Log += Client_Log;

            await client.LoginAsync(TokenType.Bot, _TOCKEN);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task Client_Log(LogMessage log)
        {
            Console.WriteLine($"{DateTime.Now} at {log.Source}] {log.Message}");
        }

        private async Task Client_Ready()
        {
            await client.SetGameAsync("BotDriveGames - Juegos y mas!!...", "", StreamType.NotStreaming);
        }

        private async Task Client_MessageReceived(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            var context = new SocketCommandContext(client, message);
                
            if(context.Message == null || string.IsNullOrWhiteSpace(context.Message.Content)) return;
            if (context.User.IsBot) return;

            int argPos = 0;
            if (!(message.HasStringPrefix("!", ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))) return;

            var result = await commands.ExecuteAsync(context, argPos);

            if (!result.IsSuccess)
            {
                Console.WriteLine($"{DateTime.Now} at commands] a ocurrido un error al ejecutar el comando. Text: {context.Message.Content} | Error: {result.Error}.");
            }
        }
    }
}
