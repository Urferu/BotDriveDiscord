using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriveBot.Resources.Database;

using Discord;

namespace DriveBot.Resources.Utils
{
    class Utils
    {
        /// <summary>
        /// Se encarga de buscar el nombre en la lista de juegos disponibles
        /// </summary>
        /// <param name="gameSearch">Corresponde al texto del juego</param>
        /// <param name="game">Parametro donde se guardará el juego encontrado</param>
        /// <returns>Regresa verdadero en caso de haber encontrado el juego</returns>
        public static bool searchGame(string gameSearch, stdClassCSharp game)
        {
            bool response = false;
            
            stdClassCSharp gamesStd = stdClassCSharp.jsonToStdClass(FilesConfig.getGames());

            for( int i = 0; i < gamesStd.toArray().Length && !response; i++)
            {
                if(gamesStd[i]["Titulo"].ToLower().Contains(gameSearch.ToLower()) || gameSearch.ToLower().Contains(gamesStd[i]["Titulo"].ToLower()))
                {
                    response = true;
                    game = gamesStd[i];
                    i = gamesStd.toArray().Length;
                }
            }

            return response;
        }

        /// <summary>
        /// Se encarga de buscar el nombre en la lista de updates disponibles
        /// </summary>
        /// <param name="updateSearch">Corresponde al texto con el titulo del update</param>
        /// <param name="indexUpdate"></param>
        /// <returns></returns>
        public static bool searchUpdate(string updateSearch, stdClassCSharp update, int indexUpdate = -1)
        {
            bool response = false;

            stdClassCSharp updatesStd = stdClassCSharp.jsonToStdClass(FilesConfig.getGames());

            return response;
        }

        public static bool searchDlc(string dlcSearch, stdClassCSharp dlc, int indexUpdate = -1)
        {
            bool response = false;

            stdClassCSharp gamesStd = stdClassCSharp.jsonToStdClass(FilesConfig.getGames());

            return response;
        }

        /// <summary>
        /// Genera el builder para el juego recibido como parametro
        /// </summary>
        /// <param name="game">Corresponde a los datos del juego</param>
        /// <param name="builderGame">Correspone al builder donde se harán las configuraciones</param>
        /// <returns>Regresa verdadero si todo se ejecuta correctamente.</returns>
        public static bool generaBuilderGame(stdClassCSharp game, EmbedBuilder builderGame)
        {
            bool respuesta = false;
            try
            {
                StringBuilder sbDatos = new StringBuilder();
                sbDatos.Append("**");
                sbDatos.Append(game["Titulo"]);
                sbDatos.Append("**");

                builderGame.WithTitle(sbDatos.ToString());

                sbDatos.Clear();

                builderGame.WithDescription(game["Descripcion"]);
                builderGame.WithColor(0xD0021B);
                builderGame.WithTimestamp(DateTimeOffset.Now);
                if (game["ImagenJuego", TiposDevolver.Boleano])
                {
                    builderGame.WithFooter(footer =>
                    {
                        footer
                            .WithText("carátule BOT by Urferu response")
                            .WithIconUrl(game["ImagenJuego"]);
                    });
                    builderGame.WithThumbnailUrl(game["ImagenJuego"])
                        .WithImageUrl(game["ImagenJuego"]);
                }

                if(game["UploadBy", TiposDevolver.Boleano])
                {
                    builderGame.WithAuthor(author =>
                    {
                        author.WithName(game["UploadBy"]).WithIconUrl(game["ImagenDiscord"]);
                    });
                }

                if (game["Peso", TiposDevolver.Boleano])
                {
                    builderGame.AddField("Peso", game["Peso"]);
                }

                if (game["Formato", TiposDevolver.Boleano])
                {
                    builderGame.AddField("Formato", game["Formato"]);
                }

                if (game["Password", TiposDevolver.Boleano])
                {
                    builderGame.AddField("Password", game["Password"]);
                }

                foreach (var link in game["Links"].toArray())
                {
                    if (sbDatos.Length > 0)
                        sbDatos.Append(Environment.NewLine);

                    sbDatos.Append(link);
                }

                builderGame.AddField("Juego", sbDatos.ToString());

                sbDatos.Clear();

                if (game["UpdateIndex", TiposDevolver.Boleano])
                {
                    game["Update"] = new stdClassCSharp();

                    if (searchUpdate(game["Titulo"], game["Update"], game["UpdateIndex",TiposDevolver.Entero])
                        && game["Update"].toArray().Length > 0)
                    {
                        stdClassCSharp links = game["Update"].Links;
                        foreach (var link in links.toArray())
                        {
                            if (sbDatos.Length > 0)
                                sbDatos.Append(Environment.NewLine);

                            sbDatos.Append(link);
                        }

                        builderGame.AddField($"Update {game["Update"].Version}", sbDatos.ToString());

                        sbDatos.Clear();
                    }
                }

                if (game["DlcIndex", TiposDevolver.Boleano])
                {
                    game["Dlc"] = new stdClassCSharp();

                    if (searchDlc(game["Titulo"], game["Dlc"], game["DlcIndex", TiposDevolver.Entero])
                        && game["Dlc"].toArray().Length > 0)
                    {
                        stdClassCSharp links = game["Dlc"].Links;
                        foreach (var link in links.toArray())
                        {
                            if (sbDatos.Length > 0)
                                sbDatos.Append(Environment.NewLine);

                            sbDatos.Append(link);
                        }

                        builderGame.AddField("DLC's", sbDatos.ToString());

                        sbDatos.Clear();
                    }
                }

                /*   .WithColor(new Color(0xD0021B))//azul 0x838AFF // blanco 0xFFFFFF*/
            }
            catch
            {
                respuesta = false;
            }
            return respuesta;
        }

        /// <summary>
        /// Genera el builder para el update recibido como parametro
        /// </summary>
        /// <param name="update">Corresponde a los datos del juego</param>
        /// <param name="builderUpdate">Correspone al builder donde se harán las configuraciones</param>
        /// <returns>Regresa verdadero si todo se ejecuta correctamente.</returns>
        public static bool generaBuilderUpdate(stdClassCSharp update, EmbedBuilder builderUpdate)
        {
            bool respuesta = false;
            try
            {
                StringBuilder sbDatos = new StringBuilder();
                sbDatos.Append("**");
                sbDatos.Append(update["Titulo"]);
                sbDatos.Append("**");

                builderUpdate.WithTitle(sbDatos.ToString());

                sbDatos.Clear();

                builderUpdate.WithDescription($"Update {update["Version"]}");
                builderUpdate.WithColor(0x838AFF);
                builderUpdate.WithTimestamp(DateTimeOffset.Now);
                if (update["ImagenJuego", TiposDevolver.Boleano])
                {
                    builderUpdate.WithFooter(footer =>
                    {
                        footer
                            .WithText("carátule BOT by Urferu response")
                            .WithIconUrl(update["ImagenJuego"]);
                    });
                    builderUpdate.WithThumbnailUrl(update["ImagenJuego"])
                        .WithImageUrl(update["ImagenJuego"]);
                }

                if (update["UploadBy", TiposDevolver.Boleano])
                {
                    builderUpdate.WithAuthor(author =>
                    {
                        author.WithName(update["UploadBy"]).WithIconUrl(update["ImagenDiscord"]);
                    });
                }

                if (update["Peso", TiposDevolver.Boleano])
                {
                    builderUpdate.AddField("Peso", update["Peso"]);
                }

                if (update["Formato", TiposDevolver.Boleano])
                {
                    builderUpdate.AddField("Formato", update["Formato"]);
                }

                if (update["Password", TiposDevolver.Boleano])
                {
                    builderUpdate.AddField("Password", update["Password"]);
                }

                foreach (var link in update["Links"].toArray())
                {
                    if (sbDatos.Length > 0)
                        sbDatos.Append(Environment.NewLine);

                    sbDatos.Append(link);
                }

                builderUpdate.AddField("Links", sbDatos.ToString());

                sbDatos.Clear();
            }
            catch
            {
                respuesta = false;
            }
            return respuesta;
        }

        /// <summary>
        /// Genera el builder para el update recibido como parametro
        /// </summary>
        /// <param name="dlc">Corresponde a los datos del juego</param>
        /// <param name="builderDlc">Correspone al builder donde se harán las configuraciones</param>
        /// <returns>Regresa verdadero si todo se ejecuta correctamente.</returns>
        public static bool generaBuilderDlc(stdClassCSharp dlc, EmbedBuilder builderDlc)
        {
            bool respuesta = false;
            try
            {
                StringBuilder sbDatos = new StringBuilder();
                sbDatos.Append("**");
                sbDatos.Append(dlc["Titulo"]);
                sbDatos.Append("**");

                builderDlc.WithTitle(sbDatos.ToString());

                sbDatos.Clear();
                
                builderDlc.WithColor(0x838AFF);
                builderDlc.WithTimestamp(DateTimeOffset.Now);
                if (dlc["ImagenJuego", TiposDevolver.Boleano])
                {
                    builderDlc.WithFooter(footer =>
                    {
                        footer
                            .WithText("carátule BOT by Urferu response")
                            .WithIconUrl(dlc["ImagenJuego"]);
                    });
                    builderDlc.WithThumbnailUrl(dlc["ImagenJuego"])
                        .WithImageUrl(dlc["ImagenJuego"]);
                }

                if (dlc["UploadBy", TiposDevolver.Boleano])
                {
                    builderDlc.WithAuthor(author =>
                    {
                        author.WithName(dlc["UploadBy"]).WithIconUrl(dlc["ImagenDiscord"]);
                    });
                }

                if (dlc["Peso", TiposDevolver.Boleano])
                {
                    builderDlc.AddField("Peso", dlc["Peso"]);
                }

                if (dlc["Formato", TiposDevolver.Boleano])
                {
                    builderDlc.AddField("Formato", dlc["Formato"]);
                }

                if (dlc["Password", TiposDevolver.Boleano])
                {
                    builderDlc.AddField("Password", dlc["Password"]);
                }

                foreach (var link in dlc["Links"].toArray())
                {
                    if (sbDatos.Length > 0)
                        sbDatos.Append(Environment.NewLine);

                    sbDatos.Append(link);
                }

                builderDlc.AddField("Links", sbDatos.ToString());

                sbDatos.Clear();
            }
            catch
            {
                respuesta = false;
            }
            return respuesta;
        }
    }
}
