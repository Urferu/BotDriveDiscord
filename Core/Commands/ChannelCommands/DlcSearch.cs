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
            if (!Context.IsPrivate)
            {
                string mensaje = inputMessage;
                int links = 0;
                string jdwonloader = "";
                stdClassCSharp dlcs = new stdClassCSharp(true);
                EmbedBuilder builderDlc;
                EmbedBuilder builderJDownloader;

                if (mensaje.Trim().Length > 3)
                {
                    if (Utils.searchDlc(mensaje, ref dlcs))
                    {
                        foreach (stdClassCSharp dlc in dlcs.toArray())
                        {
                            builderDlc = new EmbedBuilder();
                            builderJDownloader = new EmbedBuilder();
                            jdwonloader = "";
                            links = 0;
                            if (Utils.generaBuilderDlc(dlc, ref builderDlc, ref jdwonloader, ref links))
                            {
                                if (links > 1)
                                {
                                    builderJDownloader.WithTitle(builderDlc.Title);
                                    builderJDownloader.WithColor(0xFFFFFF);
                                    builderJDownloader.WithDescription(jdwonloader);
                                }
                                await Context.User.SendMessageAsync("", false, builderDlc.Build());
                                if (links > 1)
                                    await Context.User.SendMessageAsync("", false, builderJDownloader.Build());
                            }
                        }
                        if (dlcs.Count == 1)
                            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} he encontrado {dlcs.Count} resultado y te lo he enviado por mp :)");
                        else
                            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} he encontrado {dlcs.Count} resultados y te los he enviado por mp :)");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} no encontre el dlc que me pediste :(, pideselo a bello XD");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} Debes escribir mas de 3 letras de el/los dlc(s) que buscas.");
                }
            }
        }
    }
}
