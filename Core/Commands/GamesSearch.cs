using Discord;
using Discord.Commands;
using DriveBot.Resources.Utils;
using System.Threading.Tasks;

namespace DriveBot.Core.Commands
{
    public class GamesSearch : ModuleBase<SocketCommandContext>
    {
        [Command("game"), Alias("juego", "si el", "quiero el"), Summary("Solicita juego command")]
        public async Task sJustein()
        {
            string mensaje = Context.Message.Content.Replace("!game ", "").Replace("juego ", "").Replace("si el ", "").Replace("quiero el ", "").Replace(Context.Client.CurrentUser.Username, "");
            stdClassCSharp game = new stdClassCSharp();
            EmbedBuilder builderGame = new EmbedBuilder();

            if(Utils.searchGame(mensaje, game))
            {
                if(Utils.generaBuilderGame(game, builderGame))
                {
                    var embed = builderGame.Build();

                    await Context.User.SendMessageAsync(
                    null,
                    embed: embed)
                    .ConfigureAwait(false);

                    await Context.Message.DeleteAsync();
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"@{Context.User.Username} mis circuitos no me permitieron buscar tu juego correctamente :(");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync($"@{Context.User} no encontre el juego que me pediste :(");
            }
        }
    }
}
