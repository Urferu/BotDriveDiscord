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
                default:
                    await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} la tuya querras decir...");
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
    }
}
