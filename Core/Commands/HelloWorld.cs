using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

using Discord;
using Discord.Commands;

namespace DriveBot.Core.Commands
{
    public class HelloWorld : ModuleBase<SocketCommandContext>
    {
        [Command("hola"), Alias("hola", "que tal", "que hay de nuevo"), Summary("Hello world command")]
        public async Task sJustein()
        {
            switch(new Random().Next(10))
            {
                case 1:
                    await Context.Channel.SendMessageAsync($"Hola que tal!!! {Context.User.Mention}");
                    break;
                case 2:
                    await Context.Channel.SendMessageAsync($"Que cuentas {Context.User.Mention}?");
                    break;
                case 3:
                    await Context.Channel.SendMessageAsync($"Hola {Context.User.Mention} ¿Deseas algún juego?");
                    break;
                case 4:
                    await Context.Channel.SendMessageAsync($"Hey {Context.User.Mention} tengo unos cuantos juegos para ti solo pidelo.");
                    break;
                default:
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention} unos juegos?");
                    break;
            }

            
        }
    }
}
