using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using DriveBot.Resources.Utils;

namespace DriveBot.Core.Commands
{
    public class AddEditGames : ModuleBase<SocketCommandContext>
    {
        [Command("new_game"), Alias("nuevo juego"), Summary("Agrega Nuevo Juego")]
        public async Task newGame([Remainder]string inputMessage = "")
        {
            if (Context.IsPrivate)
            {
                if (stdClassCSharp.readJsonFile("usersIds.json")["users"][Context.User.Id.ToString(), TiposDevolver.Boleano])
                {
                    if (string.IsNullOrEmpty(inputMessage))
                    {
                        //await Context.User.SendMessageAsync($"tu id es: {Context.User.Id}");
                        await Context.User.SendMessageAsync($"para mandarme un juego es necesario me mandes un mensaje con el mismo comando con los siguientes datos separados por renglones:{Environment.NewLine}" +
                            $"Titulo: titulo del juego{Environment.NewLine}Descripcion: descripcion o genero del juego{Environment.NewLine}UploadBy o Uploader: quien sube el juego{Environment.NewLine}ImagenDiscord: url de la imagen del uploader{Environment.NewLine}ImagenJuego: url de la imagen del juego" +
                            $"{Environment.NewLine}Peso: tamaño que tiene el juego{Environment.NewLine}Formato: formato del juego{Environment.NewLine}Password: password del juego{Environment.NewLine}Links:{Environment.NewLine}poner todos los links del juego{Environment.NewLine}{Environment.NewLine}" +
                            $"Version o updateVersion: versión del update(Importante dejar por lo menos un renglon en blanco para ubicar cuando terminan los links del juego){Environment.NewLine}Peso: tamaño del update" +
                            $"{Environment.NewLine}Formato: formato del update{Environment.NewLine}Links: aqui todos los links del update{Environment.NewLine}{Environment.NewLine}" +
                            $"Peso: tamaño del dlc(Es importante dejar por lo menos un renglon para ubicar cuando terminan los links del update){Environment.NewLine}Formato: formato del dlc{Environment.NewLine}Links:{Environment.NewLine}links del dlc{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}" +
                            $"No es importante capturar los datos en orden a excepción de links que es el que define el final de juego, update o dlc. Hay datos del juego que se pueden omitirse para capturar pero para el juego es importante capturar titulo y links, en update version y links, en dlc solo capturar links.{Environment.NewLine}De lo contrario no se toamrá en cuenta el apartado correspondiente");
                    }
                    else
                    {
                        string anuncio = "";
                        string response = Utils.guardarJuego(inputMessage, ref anuncio);
                        await Context.User.SendMessageAsync(response);
                        if(!string.IsNullOrEmpty(anuncio))
                            await Context.Client.GetGuild(485262131797688331).GetTextChannel(485446685552803850).SendMessageAsync($"{Context.Client.GetGuild(485262131797688331).EveryoneRole.Mention} {anuncio}");
                    }
                }
                else
                {
                    await Context.User.SendMessageAsync($"No estas autorizado para utilizar este comando");
                }
            }
            else
            {
                await Context.Message.DeleteAsync(RequestOptions.Default);
                await Context.Channel.SendMessageAsync($"Ese comando solo funciona por mp.");
            }
        }

        [Command("edit_game"), Alias("editar juego"), Summary("Agrega Nuevo Juego")]
        public async Task editGame([Remainder]string inputMessage = "")
        {
            if (Context.IsPrivate)
            {
                if (stdClassCSharp.readJsonFile("usersIds")[Context.User.Id.ToString(), TiposDevolver.Boleano])
                {
                    int indexGame = 0;
                    if (string.IsNullOrEmpty(inputMessage))
                    {
                        var builder = new EmbedBuilder();
                        builder.WithTitle("Estos son los juegos que tengo disponibles:");
                        string descripcion = "";
                        foreach (stdClassCSharp juego in Utils.getListGamesEdit())
                        {
                            descripcion = $"{Environment.NewLine}{juego["indexGame"]}    {juego["Titulo"]}";
                        }
                        builder.WithDescription(descripcion);
                        builder.WithColor(new Color(0xFFF000));
                        builder.AddField("¿Para editar?", $"Solo vuelve a colocar el comando !edit_game [id del juego] en base a la lista colocada y te devolveré los datos del juego que se capturaron al agregarlo" +
                            $" después de editar esos datos mandas de nuevo el comando pero ahora de la siguiente manera:{Environment.NewLine}{Environment.NewLine}!edit_game id: id del juego{Environment.NewLine}" +
                            $"Anuncio que quieres que realice el en el discord para informar a los usuarios del cambio realizado(se puede dejar el renglon en blanco y no se enviará anuncio){Environment.NewLine}" +
                            $"Aqui los datos del juego editados.");
                        await Context.User.SendMessageAsync("", false, builder.Build());
                    }
                    else if(int.TryParse(inputMessage, out indexGame))
                    {
                        await Context.User.SendMessageAsync("Aún no me programan esta parte por favor se paciente con mi creador XD");
                    }
                    else
                    {
                        //string anuncio = "";
                        //string response = Utils.guardarJuego(inputMessage, ref anuncio);
                        //await Context.User.SendMessageAsync(response);
                        //if (!string.IsNullOrEmpty(anuncio))
                        //    await Context.Client.GetGuild(485262131797688331).GetTextChannel(485446685552803850).SendMessageAsync($"{Context.Client.GetGuild(485262131797688331).EveryoneRole.Mention} {anuncio}");
                        await Context.User.SendMessageAsync("Aún no me programan esta parte por favor se paciente con mi creador XD");
                    }
                }
                else
                {
                    await Context.User.SendMessageAsync("No estas autorizado para utilizar este comando");
                }
            }
            else
            {
                await Context.Message.DeleteAsync(RequestOptions.Default);
                await Context.Channel.SendMessageAsync($"Ese comando solo funciona por mp.");
            }
        }

        [Command("getid"), Alias("dame mi id"), Summary("Agrega Nuevo Juego")]
        public async Task getUserId([Remainder]string inputMessage = "")
        {
            if (Context.IsPrivate)
            {
                await Context.User.SendMessageAsync($"tu id es: {Context.User.Id}");
            }
            else
            {
                await Context.Message.DeleteAsync(RequestOptions.Default);
                await Context.Channel.SendMessageAsync($"Ese comando solo funciona por mp.");
            }
        }
    }
}
