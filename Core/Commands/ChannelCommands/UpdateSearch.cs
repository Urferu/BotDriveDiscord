using Discord;
using Discord.Commands;
using DriveBot.Resources.Utils;
using System.Threading.Tasks;

namespace DriveBot.Core.Commands
{
    public class UpdateSearch : ModuleBase<SocketCommandContext>
    {
        [Command("update"), Alias("update", "actualizacion", "actualización", "quiero la actualizacion del"), Summary("Solicita juego command")]
        public async Task sJustein([Remainder]string inputMessage = "NONE")
        {
            if (!Context.IsPrivate)
            {
                string mensaje = inputMessage;
                int argPos = 0;
                stdClassCSharp update = new stdClassCSharp();
                EmbedBuilder builderUpdate = new EmbedBuilder();

                if (mensaje.Trim().Length > 3)
                {
                    if (Utils.searchUpdate(mensaje, ref update))
                    {
                        if (Utils.generaBuilderUpdate(update, ref builderUpdate))
                        {
                            if (!Context.Message.HasMentionPrefix(Context.Client.CurrentUser, ref argPos))
                                await Context.Message.DeleteAsync(RequestOptions.Default);

                            await Context.User.SendMessageAsync("", false, builderUpdate.Build());
                            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} he respondido tu solicitud por mp :)");
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} mis circuitos no me permitieron buscar tu actualización correctamente :(");
                        }
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} no encontre la actualización que me pediste :(, pideselo a bello XD.");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} Debes escribir mas de 3 letras de la actualización que buscas.");
                }
            }
        }
    }
}
