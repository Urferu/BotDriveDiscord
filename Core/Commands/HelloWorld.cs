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
            switch(new Random().Next(6))
            {
                case 1:
                    await Context.Channel.SendMessageAsync($"Hola que tal!!! {Context.Message.Author.Mention}");
                    break;
                case 2:
                    await Context.Channel.SendMessageAsync($"Que cuentas {Context.Message.Author.Mention}?");
                    break;
                case 3:
                    await Context.Channel.SendMessageAsync($"Hola {Context.Message.Author.Mention} ¿Deseas algún juego?");
                    break;
                case 4:
                    await Context.Channel.SendMessageAsync($"Hey {Context.Message.Author.Mention} tengo unos cuantos juegos para ti solo pidelos.");
                    break;
                default:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} unos juegos?");
                    break;
            }
        }
    }
}
