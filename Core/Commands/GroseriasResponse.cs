using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using DriveBot.Resources.Utils;

namespace DriveBot.Core.Commands
{
    public class GroseriasResponse : ModuleBase<SocketCommandContext>
    {
        [Command("!huevos"), Alias("huevos"), Summary("Lista de juegos")]
        public async Task sHuevos()
        {
            switch (new Random().Next(1, 3))
            {
                case 1:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} te faltan...");
                    break;
                default:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} chupas...");
                    break;
            }
        }

        [Command("!chingatumadre"), Alias("chinga tu madre"), Summary("Lista de juegos")]
        public async Task sJodes()
        {
            switch (new Random().Next(1, 3))
            {
                case 1:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} que paso por que tan llevadito...");
                    break;
                case 2:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} la tuya querras decir...");
                    break;
                default:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} mejor me chingo a tu madre...");
                    break;
            }
        }

        [Command("!puto"), Alias("puto"), Summary("Lista de juegos")]
        public async Task sPirujo()
        {
            switch (new Random().Next(1, 3))
            {
                case 1:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} serás...");
                    break;
                default:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} el que lo lea...");
                    break;
            }
        }

        [Command("!cabron"), Alias("cabron", "cabrón"), Summary("Lista de juegos")]
        public async Task sCabra()
        {
            switch (new Random().Next(1, 3))
            {
                case 1:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} y de los buenos...");
                    break;
                default:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} como el need for speed cabrón, a no ese era carbón jejeje...");
                    break;
            }
        }

        [Command("!pitin"), Alias("pito corto"), Summary("Lista de juegos")]
        public async Task sPitin()
        {
            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} soy un bot no debería tener...");
        }

        [Command("!por_los_juegos"), Alias("aqui solo llegan por juegos?"), Summary("Lista de juegos")]
        public async Task juegos()
        {
            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} por desgracia y ni un gracias :(");
        }

        [Command("!gracias"), Alias("gracias"), Summary("Lista de juegos")]
        public async Task gracias()
        {
            switch (new Random().Next(1, 4))
            {
                case 1:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} de nada...");
                    break;
                case 2:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} pero que sujeto tan agradable eres...");
                    break;
                default:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} por personas asi dan ganas de seguir XD...");
                    break;
            }
        }

        [Command("!vete_verga"), Alias("vete a la verga"), Summary("respuesta groseria")]
        public async Task veteVerga()
        {
            switch (new Random().Next(1, 4))
            {
                case 1:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} Ahorita te alcanzo...");
                    break;
                case 2:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} iría pero quedó sobrepoblado cuando llegaste tú");
                    break;
                default:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} iría a la tuya pero no tienes, ups.");
                    break;
            }
        }

        [Command("!vales_verga"), Alias("vales verga"), Summary("respuesta a groseria")]
        public async Task valesVerga()
        {
            switch (new Random().Next(1, 4))
            {
                case 1:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} por lo menos valgo más que tú...");
                    break;
                case 2:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} tu ni eso vales.");
                    break;
                default:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} y quien no lo vale, mira https://es.slideshare.net/RubenSoyChido/valer-verga-costumbre-o-instinto.");
                    break;
            }
        }
    }
}
