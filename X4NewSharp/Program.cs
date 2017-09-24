using System;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Threading.Tasks;

namespace X4Sharp
{

    public class Program : ModuleBase<SocketCommandContext>

    {
        static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();
        public DiscordSocketClient _client;
        public  CommandHandler _handler;

        public async Task StartAsync()
        {
            _client = new DiscordSocketClient();
            _client.UserJoined += _client_UserJoined;
            Console.WriteLine("Starting Client");
            new CommandHandler();
            _client = new DiscordSocketClient();
            Console.WriteLine("Connecting to X4Bot");
            _client.Ready += Ready;
            string _token = "MzUwOTUxODgyNzc0MjgyMjUw.DILg9w.oJ7_m4Y0W_zXjEfEA6LWPLC7bbM";
            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();
            Console.WriteLine("Connected!");
            _handler = new CommandHandler();
            await _handler.InitializeAsync(_client);

            await Task.Delay(-1);
        }
        private async Task _client_UserJoined (SocketGuildUser user)
        {
            await Context.Channel.SendMessageAsync("Welcome to X4, " + user.Username + "!");
        }
        public async Task Ready()
        {
            await _client.SetGameAsync("x4help | X4Bot");
        }

        [Command("setstatus")]
        public async Task ModSetStatus(string input)
        {
            if (Context.User.Id == 257247527630274561 || Context.User.Id == 180376211820773377)
            {
                await Context.Client.SetGameAsync(input);
            } else
            {
                await Context.Channel.SendMessageAsync("You do not have the permission to use this command.");
            }
        }
        
    }
}



