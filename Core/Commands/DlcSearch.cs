using Discord;
using Discord.Commands;
using DriveBot.Resources.Utils;
using System.Threading.Tasks;
namespace DriveBot.Core.Commands
{
    public class DlcSearch : ModuleBase<SocketCommandContext>
    {
        [Command("dlc"), Alias("dlc", "ocupo el dlc del"), Summary("Solicita juego command")]
        public async Task sJustein([Remainder]string inputMessage = "NONE")
        {
            string mensaje = inputMessage;
            stdClassCSharp dlc = new stdClassCSharp();
            EmbedBuilder builderDlc = new EmbedBuilder();

            if (Utils.searchDlc(mensaje, ref dlc))
            {
                if (Utils.generaBuilderDlc(dlc, ref builderDlc))
                {
                    await Context.User.SendMessageAsync("", false, builderDlc.Build());
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention} mis circuitos no me permitieron buscar tu dlc correctamente :(");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} no encontre el dlc que me pediste :(");
            }
        }
    }
}
