using Discord;
using Discord.Commands;
using DriveBot.Resources.Utils;
using System.Threading.Tasks;

namespace DriveBot.Core.Commands
{
    public class UpdateSearch : ModuleBase<SocketCommandContext>
    {
        [Command("update"), Alias("actualizacion", "actualización", "quiero la actualizacion del"), Summary("Solicita juego command")]
        public async Task sJustein()
        {
            string mensaje = Context.Message.Content.Replace("!update ", "").Replace("actualizacion ", "").Replace("actualización ", "").Replace("quiero la actualizacion del ", "").Replace(Context.Client.CurrentUser.Username, "");
            stdClassCSharp update = new stdClassCSharp();
            EmbedBuilder builderUpdate = new EmbedBuilder();

            if (Utils.searchUpdate(mensaje, update))
            {
                if (Utils.generaBuilderGame(update, builderUpdate))
                {
                    var embed = builderUpdate.Build();

                    await Context.User.SendMessageAsync(
                    null,
                    embed: embed)
                    .ConfigureAwait(false);

                    await Context.Message.DeleteAsync();
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"@{Context.User.Username} mis circuitos no me permitieron buscar tu actualización correctamente :(");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync($"@{Context.User} no encontre la actualización que me pediste :(");
            }
        }
    }
}
