using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using DriveBot.Resources.Utils;

namespace DriveBot.Core.Commands
{
    public class HelpResponse : ModuleBase<SocketCommandContext>
    {
        [Command("help"), Alias("help", "ayuda", "donde comienzo?", "como descargo los juegos?"), Summary("Muestra la ayuda")]
        public async Task sJustein()
        {
            if (!Context.IsPrivate)
            {
                stdClassCSharp help = stdClassCSharp.readJsonFile("helpText.json");
                if (help.Count > 0)
                {
                    var builder = new EmbedBuilder()
                    .WithTitle((help["title"] as string))
                    .WithDescription((help["description"] as string).Replace("{user}", Context.User.Mention))
                    .WithColor(new Color(0xEAFF00))
                    .WithFooter(footer =>
                    {
                        footer
                            .WithText((help["footer"]["text"] as string))
                            .WithIconUrl((help["footer"]["iconUrl"] as string));
                    })
                    .WithAuthor(author =>
                    {
                        author
                            .WithName((help["author"]["name"] as string))
                            .WithIconUrl((help["author"]["iconUrl"] as string));
                    });
                    await Context.Message.DeleteAsync(RequestOptions.Default);
                    await Context.Channel.SendMessageAsync("", false, builder.Build());
                }
            }
        }

        [Command("get_help"), Alias("traime la estructura de la ayuda", "estructura ayuda"), Summary("Muestra la estructura de la ayuda")]
        public async Task getHelp()
        {
            if (Context.IsPrivate)
            {
                if (stdClassCSharp.readJsonFile("usersIds.json")["users"][Context.User.Id.ToString(), TiposDevolver.Boleano])
                {
                    stdClassCSharp help = stdClassCSharp.readJsonFile("helpText.json");
                    if (help.Count > 0)
                    {
                        await Context.Channel.SendMessageAsync(help.jsonValue);
                    }
                }
                else
                {
                    await Context.User.SendMessageAsync("No estas autorizado para utilizar este comando");
                }
            }
            else
            {
                await Context.Message.DeleteAsync(RequestOptions.Default);
                await Context.Channel.SendMessageAsync($"Ese comando solo funciona por mp.");
            }
        }

        [Command("set_help"), Alias("cambia la estructura de la ayuda por", "cambio ayuda"), Summary("Cambia la estructura de la ayuda")]
        public async Task setHelp([Remainder]string inputMessage = "")
        {
            if (Context.IsPrivate)
            {
                if (stdClassCSharp.readJsonFile("usersIds.json")["users"][Context.User.Id.ToString(), TiposDevolver.Boleano])
                {
                    stdClassCSharp help = stdClassCSharp.jsonToStdClass(inputMessage.Trim());
                    if (help.Count > 0)
                    {
                        help.writeJsonFile("helpText.json");
                        await Context.Channel.SendMessageAsync("Se ha modificado la ayuda correctamente");
                    }
                }
                else
                {
                    await Context.User.SendMessageAsync("No estas autorizado para utilizar este comando");
                }
            }
            else
            {
                await Context.Message.DeleteAsync(RequestOptions.Default);
                await Context.Channel.SendMessageAsync($"Ese comando solo funciona por mp.");
            }
        }
    }
}
