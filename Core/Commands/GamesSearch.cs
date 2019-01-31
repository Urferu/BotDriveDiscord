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
            string mensaje = inputMessage;
            stdClassCSharp game = new stdClassCSharp();
            EmbedBuilder builderGame = new EmbedBuilder();

            if(Utils.searchGame(mensaje,ref game))
            {
                if(Utils.generaBuilderGame(game,ref builderGame))
                {
                    await Context.User.SendMessageAsync("", false, builderGame.Build());
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention} mis circuitos no me permitieron buscar tu juego correctamente :(");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} no encontre el juego que me pediste :(");
            }
        }
    }
}
