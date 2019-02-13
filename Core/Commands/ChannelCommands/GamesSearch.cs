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
                stdClassCSharp game = new stdClassCSharp();
                EmbedBuilder builderGame = new EmbedBuilder();

                if (mensaje.Trim().Length > 3)
                {
                    if (Utils.searchGame(mensaje, ref game))
                    {
                        if (Utils.generaBuilderGame(game, ref builderGame))
                        {
                            //if (!Context.Message.HasMentionPrefix(Context.Client.CurrentUser, ref argPos))
                            //    await Context.Message.DeleteAsync(RequestOptions.Default);

                            await Context.User.SendMessageAsync("", false, builderGame.Build());
                            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} he respondido tu solicitud por mp :)");
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} mis circuitos no me permitieron buscar tu juego correctamente :(");
                        }
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
