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
                int argPos = 0;
                stdClassCSharp dlcs = new stdClassCSharp(true);
                EmbedBuilder builderDlc;

                if (mensaje.Trim().Length > 3)
                {
                    if (Utils.searchDlc(mensaje, ref dlcs))
                    {
                        builderDlc = new EmbedBuilder();
                        foreach (stdClassCSharp dlc in dlcs.toArray())
                        {
                            if (Utils.generaBuilderDlc(dlc, ref builderDlc))
                            {
                                await Context.User.SendMessageAsync("", false, builderDlc.Build());
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
