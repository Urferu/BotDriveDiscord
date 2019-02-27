using System;
using System.Threading.Tasks;
using System.Collections;

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
                        Discord.WebSocket.SocketGuild switchGeneral = Context.Client.GetGuild(485262131797688331);

                        string anuncio = "";
                        string trailer = "";
                        string response = Utils.guardarJuego(inputMessage, ref anuncio, ref trailer);
                        await Context.User.SendMessageAsync($"{Context.User.Mention} {response}");
                        anuncio = /*switchGeneral.EveryoneRole.Mention + */"@everyone " + anuncio;
                        if (!string.IsNullOrWhiteSpace(anuncio))
                        {
                            //await Context.Client.GetGuild(485262131797688331).GetTextChannel(485446685552803850).SendMessageAsync(anuncioC);
                            await switchGeneral.GetTextChannel(543276952627314717).SendMessageAsync(anuncio);
                        }
                        if (!string.IsNullOrWhiteSpace(trailer))
                        {
                            await switchGeneral.GetTextChannel(543276952627314717).SendMessageAsync(trailer);
                        }
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
                if (stdClassCSharp.readJsonFile("usersIds.json")["users"][Context.User.Id.ToString(), TiposDevolver.Boleano])
                {
                    int indexGame = 0;
                    if (string.IsNullOrEmpty(inputMessage))
                    {
                        var builder = new EmbedBuilder();
                        int fields = 0;
                        builder.WithTitle("Ids de los juegos:");
                        builder.WithColor(new Color(0xFFF000));
                        foreach (stdClassCSharp juego in Utils.getListGamesEdit())
                        {
                            if (fields >= 24)
                            {
                                await Context.User.SendMessageAsync("", false, builder.Build());
                                builder = new EmbedBuilder();
                                builder.WithColor(new Color(0xFFF000));
                                fields = 0;
                            }
                            builder.AddField(juego["indexGame", TiposDevolver.Cadena], juego["Titulo"]);
                            fields++;
                        }
                        if (fields >= 24)
                        {
                            await Context.User.SendMessageAsync("", false, builder.Build());
                            builder = new EmbedBuilder();
                            builder.WithColor(new Color(0xFFF000));
                            fields = 0;
                        }
                        builder.AddField("¿Para editar?", $"Solo vuelve a colocar el comando !edit_game [id del juego] en base a la lista colocada y te devolveré los datos del juego que se capturaron al agregarlo" +
                            $" después de editar esos datos mandas de nuevo el comando pero ahora de la siguiente manera:{Environment.NewLine}{Environment.NewLine}!edit_game id: id del juego{Environment.NewLine}" +
                            $"Anuncio que quieres que realice el en el discord para informar a los usuarios del cambio realizado(se puede dejar el renglon en blanco y no se enviará anuncio){Environment.NewLine}" +
                            $"Aqui los datos del juego editados.");
                        await Context.User.SendMessageAsync("", false, builder.Build());
                    }
                    else if(int.TryParse(inputMessage, out indexGame))
                    {
                        string response = Utils.searchGameToEdit(indexGame);
                        await Context.User.SendMessageAsync(response);
                    }
                    else
                    {
                        Discord.WebSocket.SocketGuild switchGeneral = Context.Client.GetGuild(485262131797688331);
                        ArrayList datosJuego = new ArrayList(inputMessage.Split('\n'));
                        if(datosJuego.Count == 1)
                        {
                            datosJuego = new ArrayList(inputMessage.Split(new String[] { Environment.NewLine }, StringSplitOptions.None));
                        }
                        int.TryParse(datosJuego[0].ToString().Substring(datosJuego[0].ToString().IndexOf(':') + 1), out indexGame);
                        string anuncio = datosJuego[1].ToString().Trim();
                        string trailer = "";
                        datosJuego.RemoveAt(0);
                        datosJuego.RemoveAt(0);
                        string response = string.Join("\n", datosJuego.ToArray());
                        response = Utils.guardarJuego(response, ref anuncio, ref trailer, indexGame);
                        await Context.User.SendMessageAsync($"{Context.User.Mention} {response}");
                        if(!string.IsNullOrWhiteSpace(anuncio))
                        {
                            await switchGeneral.GetTextChannel(543276952627314717).SendMessageAsync($"@everyone {anuncio}");
                        }
                        if (!string.IsNullOrWhiteSpace(trailer))
                        {
                            await switchGeneral.GetTextChannel(543276952627314717).SendMessageAsync(trailer);
                        }
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

        [Command("delete_game"), Alias("borrar juego"), Summary("Elimina un juego")]
        public async Task deleteGame([Remainder]string inputMessage = "")
        {
            if (Context.IsPrivate)
            {
                if (stdClassCSharp.readJsonFile("usersIds.json")["users"][Context.User.Id.ToString(), TiposDevolver.Boleano])
                {
                    int indexGame = 0;
                    if (string.IsNullOrEmpty(inputMessage))
                    {
                        var builder = new EmbedBuilder();
                        int fields = 0;
                        builder.WithTitle("Ids de los juegos:");
                        builder.WithColor(new Color(0xFFF000));
                        foreach (stdClassCSharp juego in Utils.getListGamesEdit())
                        {
                            if (fields >= 24)
                            {
                                await Context.User.SendMessageAsync("", false, builder.Build());
                                builder = new EmbedBuilder();
                                builder.WithColor(new Color(0xFFF000));
                                fields = 0;
                            }
                            builder.AddField(juego["indexGame", TiposDevolver.Cadena], juego["Titulo"]);
                            fields++;
                        }
                        if (fields >= 24)
                        {
                            await Context.User.SendMessageAsync("", false, builder.Build());
                            builder = new EmbedBuilder();
                            builder.WithColor(new Color(0xFFF000));
                            fields = 0;
                        }
                        builder.AddField("¿Para borrar?", $"Solo vuelve a colocar el comando !delete_game [id del juego] en base a la lista colocada y se eliminará el juego seleccionadp.");
                        await Context.User.SendMessageAsync("", false, builder.Build());
                    }
                    else if (int.TryParse(inputMessage, out indexGame))
                    {
                        string response = Utils.deleteGame(indexGame);
                        await Context.User.SendMessageAsync(response);
                    }
                    else
                    {
                        await Context.User.SendMessageAsync("Algo no enviaste bien");
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

        [Command("getid"), Alias("dame mi id"), Summary("Obtiene tu id")]
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

        [Command("clear_messages"), Alias("limpia tus mensajes"), Summary("limpia los mensajes")]
        public async Task clearMessages()
        {
            //if (Context.IsPrivate)
            //{
            //    try
            //    {
            //        var message = Context.Message.Channel.GetMessagesAsync(Context.Message.Id, Direction.Before, 1).Flatten().Result;
            //
            //        while (message != null)
            //        {
            //            try
            //            {
            //                await Context.Channel.DeleteMessagesAsync(message, RequestOptions.Default);
            //                
            //            }
            //            catch
            //            {
            //            }
            //        }
            //    }
            //    catch
            //    {
            //    }
            //}
            //else
            //{
            //    await Context.Message.DeleteAsync(RequestOptions.Default);
            //    await Context.Channel.SendMessageAsync($"Ese comando solo funciona por mp.");
            //}
        }

        [Command("get_avatar"), Alias("dame el avatar de"), Summary("Obtiene el avatar del usuario mencionado")]
        public async Task getAvatarUser([Remainder]string inputMessage = "")
        {
            //if (Context.IsPrivate)
            //{
            //    await Context.User.SendMessageAsync($"tu id es: {Context.User.Id}");
            //}
            //else
            //{
            //    await Context.Message.DeleteAsync(RequestOptions.Default);
            //    await Context.Channel.SendMessageAsync($"Ese comando solo funciona por mp.");
            //}
        }
    }
}
