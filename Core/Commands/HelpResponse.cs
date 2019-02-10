using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using DriveBot.Resources.Utils;

namespace DriveBot.Core.Commands
{
    public class HelpResponse : ModuleBase<SocketCommandContext>
    {
        [Command("help"), Alias("help", "ayuda", "donde comienzo?", "como descargo los juegos?"), Summary("Lista de juegos")]
        public async Task sJustein()
        {
            var builder = new EmbedBuilder()
                .WithTitle("Este es el sitema de ayuda")
                .WithDescription($"{Context.Message.Author.Mention} Debes ir al [#canal-comandos-del-bot](https://discordapp.com/channels/485262131797688331/532600119636525087) para ver la lista de comandos que tiene el bot para apoyarte," +
                    $" luego a [#el-rincon-de-miyagi](https://discordapp.com/channels/485262131797688331/517064255569264690) para pedir la lista de juegos y en caso de que el juego no se encuentre dirigete hacia el canal "+
                    $"[#peticiones](https://discordapp.com/channels/485262131797688331/536315268180344842) y haz el pedido de juegos.")
                .WithColor(new Color(0xF3FF00))
                .WithFooter(footer => {
                    footer
                        .WithText("Esta ayuda fue solicitada e ideada por Puro Macho! (el de las buenas ideas)");
                })
                .WithAuthor(author => {
                    author
                        .WithName("By Puro Macho!")
                        .WithIconUrl("https://cdn.discordapp.com/avatars/507637105749524480/14a8fb1947df8cdc7d63b85cb8e2c9ed.png");
                });
            var embed = builder.Build();
            await Context.Message.DeleteAsync(RequestOptions.Default);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
    }
}
