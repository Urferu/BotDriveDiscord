using Discord;
using Discord.Commands;
using DriveBot.Resources.Utils;
using System;
using System.Threading.Tasks;

namespace DriveBot.Core.Commands
{
    public class ListGames : ModuleBase<SocketCommandContext>
    {
        [Command("list"), Alias("lista", "dame la lista de juegos", "que juegos tienes?"), Summary("Lista de juegos")]
        public async Task sJustein([Remainder]string inputMessage = "NONE")
        {
            if (!Context.IsPrivate || stdClassCSharp.readJsonFile("usersIds.json")["users"][Context.User.Id.ToString(), TiposDevolver.Boleano])
            {
                var builder = new EmbedBuilder();
                int fields = 0;
                builder.WithTitle("Estos son los juegos que tengo disponibles:");
                //.WithDescription(Utils.getListGames())
                builder.WithColor(new Color(0xAF3A));
                foreach(string listaLetra in Utils.getListGames())
                {
                    if(fields >= 24)
                    {
                        if (inputMessage.ToLower() == "aqui")
                            await Context.Channel.SendMessageAsync("", false, builder.Build());
                        else
                            await Context.User.SendMessageAsync("", false, builder.Build());

                        builder = new EmbedBuilder();
                        builder.WithColor(new Color(0xAF3A));
                        fields = 0;
                    }
                    builder.AddField(listaLetra.Substring(0, 1), listaLetra);
                    fields++;
                }

                if (fields >= 24)
                {
                    if (inputMessage.ToLower() == "aqui")
                        await Context.Channel.SendMessageAsync("", false, builder.Build());
                    else
                        await Context.User.SendMessageAsync("", false, builder.Build());

                    builder = new EmbedBuilder();
                    builder.WithColor(new Color(0xAF3A));
                    fields = 0;
                }

                builder.AddField("¿Como pedir juegos?", $"Puedes pedirme juegos, updates y dlc solamente buscando" +
                    $" por una parte de su titulo de la siguiente manera:{Environment.NewLine}" +
                    $"Para pedirme un juego puedes utilizar el comando (!game juego_que_deseas) o puedes mencionarme y" +
                    $" agregar las frases (juego juego_que_deseas), (si el juego_que_deseas) o (quiero el juego_que_deseas).{Environment.NewLine}{Environment.NewLine}" +
                    $"Para pedirme una actualización o update puedes utilizar el comando (!update nombre_juego_del_update) o puedes mencionarme y agregar las frases" +
                    $" (update nombre_juego_del_update), (quiero el update del nombre_juego_del_update), (actualizacion nombre_juego_del_update), (actualización nombre_juego_del_update) o (quiero la actualizacion del nombre_juego_del_update)" +
                    $".{Environment.NewLine}{Environment.NewLine}Para pedirme los dlc's de un juego puedes utilizar el comando (!dlc nombre_juego_del_dlc) o puedes mencionarme " +
                    $"y agregar las frases (dlc nombre_juego_del_dlc), (ocupo el dlc del nombre_juego_del_dlc), (ocupo los dlc del nombre_juego_del_dlc) o (quiero los dlc del nombre_juego_del_dlc)." +
                    $"{Environment.NewLine}{Environment.NewLine} Espero te sea mas comodo pedirme tus juegos.");


                if (inputMessage.ToLower() == "aqui")
                {
                    if(!Context.IsPrivate)
                        await Context.Message.DeleteAsync(RequestOptions.Default);
                    await Context.Channel.SendMessageAsync("", false, builder.Build());
                }
                else
                {
                    if (!Context.IsPrivate)
                    {
                        await Context.Message.DeleteAsync(RequestOptions.Default);
                        await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} he respondido tu solicitud por mp :)");
                    }
                    await Context.User.SendMessageAsync("", false, builder.Build());
                    
                }
            }
            else
            {
                await Context.User.SendMessageAsync("Este comando no funciona por mp o no estas autorizado para utilizarlo.");
            }
        }
    }
}
