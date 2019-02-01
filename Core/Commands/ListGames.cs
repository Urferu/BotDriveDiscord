using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using DriveBot.Resources.Utils;

namespace DriveBot.Core.Commands
{
    public class ListGames : ModuleBase<SocketCommandContext>
    {
        [Command("list"), Alias("lista", "dame la lista de juegos", "que juegos tienes?"), Summary("Lista de juegos")]
        public async Task sJustein([Remainder]string inputMessage = "NONE")
        {
            string response = $"{Context.Message.Author.Mention} Estos son los juegos que tengo disponibles:{Environment.NewLine}{Utils.getListGames()}{Environment.NewLine}{Environment.NewLine}Puedes pedirme juegos, updates y dlc solamente buscando" +
                    $" por una parte de su titulo de la siguiente manera:{Environment.NewLine}" +
                    $"Para pedirme un juego puedes utilizar el comando (!game juego_que_deseas) o puedes mencionarme y" +
                    $" agregar las frases (juego juego_que_deseas), (si el juego_que_deseas) o (quiero el juego_que_deseas).{Environment.NewLine}{Environment.NewLine}" +
                    $"Para pedirme una actualización o update puedes utilizar el comando (!update nombre_juego_del_update) o puedes mencionarme y agregar las frases" +
                    $" (update nombre_juego_del_update), (quiero el update del nombre_juego_del_update), (actualizacion nombre_juego_del_update), (actualización nombre_juego_del_update) o (quiero la actualizacion del nombre_juego_del_update)" +
                    $".{Environment.NewLine}{Environment.NewLine}Para pedirme los dlc's de un juego puedes utilizar el comando (!dlc nombre_juego_del_dlc) o puedes mencionarme " +
                    $"y agregar las frases (dlc nombre_juego_del_dlc), (ocupo el dlc del nombre_juego_del_dlc), (ocupo los dlc del nombre_juego_del_dlc) o (quiero los dlc del nombre_juego_del_dlc)." +
                    $"{Environment.NewLine}{Environment.NewLine} Espero te sea mas comodo pedirme tus juegos.";
            if (inputMessage.ToLower() == "aqui")
            {
                await Context.Message.DeleteAsync(RequestOptions.Default);
                await Context.Channel.SendMessageAsync(response);
            }
            else
            {
                await Context.Message.DeleteAsync(RequestOptions.Default);
                await Context.User.SendMessageAsync(response);
                await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} he respondido tu solicitud por mp :)");
            }
        }
    }
}
