using Discord;
using Discord.Commands;
using DriveBot.Resources.Utils;
using System.Threading.Tasks;
namespace DriveBot.Core.Commands
{
    public class DlcSearch : ModuleBase<SocketCommandContext>
    {
        [Command("dlc"), Alias("dlc", "ocupo el dlc del"), Summary("Solicita juego command")]
        public async Task sJustein()
        {
            string mensaje = Context.Message.Content.Replace("!dlc ", "").Replace("dlc ", "").Replace("ocupo el dlc del ", "").Replace(Context.Client.CurrentUser.Username, "");
            stdClassCSharp dlc = new stdClassCSharp();
            EmbedBuilder builderDlc = new EmbedBuilder();

            if (Utils.searchDlc(mensaje, dlc))
            {
                if (Utils.generaBuilderDlc(dlc, builderDlc))
                {
                    var embed = builderDlc.Build();

                    await Context.User.SendMessageAsync(
                    null,
                    embed: embed)
                    .ConfigureAwait(false);
                    
                    await Context.Message.DeleteAsync();
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"@{Context.User.Username} mis circuitos no me permitieron buscar tu dlc correctamente :(");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync($"@{Context.User} no encontre el dlc que me pediste :(");
            }
        }
    }
}
