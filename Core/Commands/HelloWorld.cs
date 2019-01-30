using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace DriveBot.Core.Commands
{
    public class HelloWorld : ModuleBase<SocketCommandContext>
    {
        [Command("hello"), Alias("helloworld", "world"), Summary("Hello world command")]
        public async Task sJustein()
        {
            await Context.Channel.SendMessageAsync("Hola que tal!!!");
        }
    }
}
