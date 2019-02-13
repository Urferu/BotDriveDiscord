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
                stdClassCSharp dlc = new stdClassCSharp();
                EmbedBuilder builderDlc = new EmbedBuilder();

                if (mensaje.Trim().Length > 3)
                {
                    if (Utils.searchDlc(mensaje, ref dlc))
                    {
                        if (Utils.generaBuilderDlc(dlc, ref builderDlc))
                        {
                            //if (!Context.Message.HasMentionPrefix(Context.Client.CurrentUser, ref argPos))
                            //    await Context.Message.DeleteAsync(RequestOptions.Default);

                            await Context.User.SendMessageAsync("", false, builderDlc.Build());
                            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} he respondido tu solicitud por mp :)");
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} mis circuitos no me permitieron buscar tu dlc correctamente :(");
                        }
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
