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
                stdClassCSharp updates = new stdClassCSharp(true);
                EmbedBuilder builderUpdate;

                if (mensaje.Trim().Length > 3)
                {
                    if (Utils.searchUpdate(mensaje, ref updates))
                    {
                        foreach(stdClassCSharp update in updates.toArray())
                        {
                            builderUpdate = new EmbedBuilder();
                            if (Utils.generaBuilderUpdate(update, ref builderUpdate))
                            {
                                await Context.User.SendMessageAsync("", false, builderUpdate.Build());
                            }
                        }
                        if (updates.Count == 1)
                            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} he encontrado {updates.Count} resultado y te lo he enviado por mp :)");
                        else
                            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} he encontrado {updates.Count} resultados y te los he enviado por mp :)");
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
