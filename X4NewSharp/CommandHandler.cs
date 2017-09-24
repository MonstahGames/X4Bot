using Discord.WebSocket;
using Discord.Commands;
using System.Threading.Tasks;
using System.Reflection;

namespace X4Sharp
{
    public class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _service;
        public async Task InitializeAsync(DiscordSocketClient client)
        {
            _client = client;

            _service = new CommandService();

            await _service.AddModulesAsync(Assembly.GetEntryAssembly());

            _client.MessageReceived += HandleCommandAsync;
        }    
        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;
            var context = new SocketCommandContext(_client, msg);
            int argPos = 0;
            if (msg.HasStringPrefix("x4", ref argPos))
            {
                var result = await _service.ExecuteAsync(context, argPos);
                //&& result.Error != CommandError.UnknownCommand
                if (!result.IsSuccess)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }
    }
}