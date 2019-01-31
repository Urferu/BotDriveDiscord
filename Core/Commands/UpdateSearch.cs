using Discord;
using Discord.Commands;
using DriveBot.Resources.Utils;
using System.Threading.Tasks;

namespace DriveBot.Core.Commands
{
    public class UpdateSearch : ModuleBase<SocketCommandContext>
    {
        [Command("update"), Alias("actualizacion", "actualización", "quiero la actualizacion del"), Summary("Solicita juego command")]
        public async Task sJustein([Remainder]string inputMessage = "NONE")
        {
            string mensaje = inputMessage;
            stdClassCSharp update = new stdClassCSharp();
            EmbedBuilder builderUpdate = new EmbedBuilder();

            if (Utils.searchUpdate(mensaje, ref update))
            {
                if (Utils.generaBuilderUpdate(update, ref builderUpdate))
                {
                    await Context.User.SendMessageAsync("", false, builderUpdate.Build());
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention} mis circuitos no me permitieron buscar tu actualización correctamente :(");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} no encontre la actualización que me pediste :(");
            }
        }
    }
}
