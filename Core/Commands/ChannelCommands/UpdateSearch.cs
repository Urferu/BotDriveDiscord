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
                int links = 0;
                string jdwonloader = "";
                stdClassCSharp updates = new stdClassCSharp(true);
                EmbedBuilder builderUpdate;
                EmbedBuilder builderJDownloader;

                if (mensaje.Trim().Length > 3)
                {
                    if (Utils.searchUpdate(mensaje, ref updates))
                    {
                        foreach(stdClassCSharp update in updates.toArray())
                        {
                            builderUpdate = new EmbedBuilder();
                            builderJDownloader = new EmbedBuilder();
                            jdwonloader = "";
                            links = 0;
                            if (Utils.generaBuilderUpdate(update, ref builderUpdate, ref jdwonloader, ref links))
                            {
                                if (links > 1)
                                {
                                    builderJDownloader.WithTitle(builderUpdate.Title);
                                    builderJDownloader.WithColor(0x838AFF);
                                    builderJDownloader.WithDescription(jdwonloader);
                                }
                                await Context.User.SendMessageAsync("", false, builderUpdate.Build());
                                if (links > 1)
                                    await Context.User.SendMessageAsync("", false, builderJDownloader.Build());
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
