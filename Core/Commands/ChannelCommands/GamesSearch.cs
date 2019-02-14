using Discord;
using Discord.Commands;
using DriveBot.Resources.Utils;
using System.Threading.Tasks;

namespace DriveBot.Core.Commands
{
    public class GamesSearch : ModuleBase<SocketCommandContext>
    {
        [Command("game"), Alias("juego", "si el", "quiero el"), Summary("Solicita juego command")]
        public async Task sJustein([Remainder]string inputMessage = "NONE")
        {
            if (!Context.IsPrivate)
            {
                string mensaje = inputMessage;
                int argPos = 0;
                stdClassCSharp games = new stdClassCSharp(true);
                EmbedBuilder builderGame;

                if (mensaje.Trim().Length > 3)
                {
                    if (Utils.searchGame(mensaje, ref games))
                    {
                        foreach(stdClassCSharp game in games.toArray())
                        {
                            builderGame = new EmbedBuilder();
                            if (Utils.generaBuilderGame(game, ref builderGame))
                            {
                                await Context.User.SendMessageAsync("", false, builderGame.Build());
                            }
                        }
                        if(games.Count == 1)
                            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} he encontrado {games.Count} resultado y te lo he enviado por mp :)");
                        else
                            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} he encontrado {games.Count} resultados y te los he enviado por mp :)");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} no encontre el juego que me pediste :(, pideselo a bello XD.");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} Debes escribir mas de 3 letras de el juego que buscas.");
                }
            }
        }
    }
}
