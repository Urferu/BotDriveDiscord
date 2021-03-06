﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;

namespace DriveBot.Resources.Utils
{
    class Utils
    {
        /// <summary>
        /// Se encarga de buscar el nombre en la lista de juegos disponibles
        /// </summary>
        /// <param name="gameSearch">Corresponde al texto del juego</param>
        /// <param name="games">Parametro donde se guardará el juego encontrado</param>
        /// <returns>Regresa verdadero en caso de haber encontrado el juego</returns>
        public static bool searchGame(string gameSearch,ref stdClassCSharp games, int indexGame = -1)
        {
            bool response = false;

            stdClassCSharp gamesStd = stdClassCSharp.readJsonFile("games.json");

            if (indexGame >= 0)
            {
                games = gamesStd[indexGame];
                response = true;
            }
            else
            {
                for (int i = 0; i < gamesStd.toArray().Length; i++)
                {
                    if (gamesStd[i]["Titulo"].ToLower().Contains(gameSearch.ToLower()))
                    {
                        response = true;
                        games.Add(gamesStd[i]);
                    }
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
        public static bool searchUpdate(string updateSearch, ref stdClassCSharp update, int indexUpdate = -1)
        {
            bool response = false;

            stdClassCSharp updatesStd = stdClassCSharp.readJsonFile("updates.json");

            if(indexUpdate >= 0)
            {
                update = updatesStd[indexUpdate];
                response = true;
            }
            else
            {
                for (int i = 0; i < updatesStd.toArray().Length; i++)
                {
                    if (updatesStd[i]["Titulo"].ToLower().Contains(updateSearch.ToLower()))
                    {
                        response = true;
                        update.Add(updatesStd[i]);
                    }
                }
            }

            return response;
        }

        public static bool searchDlc(string dlcSearch, ref stdClassCSharp dlc, int indexDlc = -1)
        {
            bool response = false;

            stdClassCSharp dlcStd = stdClassCSharp.readJsonFile("dlcs.json");

            if (indexDlc >= 0)
            {
                dlc = dlcStd[indexDlc];
                response = true;
            }
            else
            {
                for (int i = 0; i < dlcStd.toArray().Length; i++)
                {
                    if (dlcStd[i]["Titulo"].ToLower().Contains(dlcSearch.ToLower()) || dlcSearch.ToLower().Contains(dlcStd[i]["Titulo"].ToLower()))
                    {
                        response = true;
                        dlc.Add(dlcStd[i]);
                    }
                }
            }

            return response;
        }

        public static List<string> getListGames()
        {
            var responses = new List<string>();
            StringBuilder response = new StringBuilder();
            string Letter = "";

            stdClassCSharp gamesStd = stdClassCSharp.readJsonFile("games.json");

            List<stdClassCSharp> listaJuegos = new List<stdClassCSharp>();

            foreach (stdClassCSharp game in gamesStd.toArray())
            {
                listaJuegos.Add(game);
            }

            listaJuegos = listaJuegos.OrderBy(lj => lj["Titulo"]).ToList();

            Letter = listaJuegos[0]["Titulo"].Substring(0, 1);

            foreach (stdClassCSharp game in listaJuegos)
            {
                if (response.Length > 0)
                    response.Append(Environment.NewLine);

                if(Letter != game["Titulo"].Substring(0,1))
                {
                    responses.Add(response.ToString().Trim());
                    response.Clear();
                    Letter = game["Titulo"].Substring(0, 1);
                }
                response.Append(game["Titulo"]);
            }

            responses.Add(response.ToString().Trim());
            response.Clear();

            return responses;
        }

        /// <summary>
        /// Devuelve la lista de juegos a un usuario que quiera editar un juego
        /// </summary>
        /// <returns></returns>
        public static List<stdClassCSharp> getListGamesEdit()
        {

            stdClassCSharp gamesStd = stdClassCSharp.readJsonFile("games.json");

            List<stdClassCSharp> listaJuegos = new List<stdClassCSharp>();

            foreach (stdClassCSharp game in gamesStd.toArray())
            {
                game["indexGame"] = listaJuegos.Count;
                listaJuegos.Add(game);
            }

            return listaJuegos.OrderBy(lj => lj["Titulo"]).ToList();
        }

        /// <summary>
        /// Devuelve la lista de juegos a un usuario que quiera editar un juego
        /// </summary>
        /// <returns></returns>
        public static string searchGameToEdit(int indexGame)
        {
            StringBuilder sbResponse = new StringBuilder();
            stdClassCSharp game = new stdClassCSharp();
            stdClassCSharp update = new stdClassCSharp();
            stdClassCSharp dlc = new stdClassCSharp();
            sbResponse.Append("id: ");
            sbResponse.Append(indexGame);
            sbResponse.Append(Environment.NewLine);
            sbResponse.Append("Aqui coloca el anuncio que quieres que haga para informarle a los usuarios del cambio");
            sbResponse.Append(Environment.NewLine);
            if (searchGame("", ref game, indexGame))
            {
                sbResponse.Append("Titulo: ");
                sbResponse.Append(game["Titulo"]);
                sbResponse.Append(Environment.NewLine);

                if (game["Descripcion", TiposDevolver.Boleano])
                {
                    sbResponse.Append("Descripcion: ");
                    sbResponse.Append(game["Descripcion"]);
                    sbResponse.Append(Environment.NewLine);
                }
                else
                {
                    sbResponse.Append("Descripcion: (No capturaste la vez anterior, si no actualizarás este campo eliminalo de los datos.)");
                    sbResponse.Append(Environment.NewLine);
                }

                if (game["UploadBy", TiposDevolver.Boleano])
                {
                    sbResponse.Append("UploadBy: ");
                    sbResponse.Append(game["UploadBy"]);
                    sbResponse.Append(Environment.NewLine);
                }
                else
                {
                    sbResponse.Append("UploadBy: (No capturaste la vez anterior, si no actualizarás este campo eliminalo de los datos.)");
                    sbResponse.Append(Environment.NewLine);
                }

                if (game["ImagenDiscord", TiposDevolver.Boleano])
                {
                    sbResponse.Append("ImagenDiscord: ");
                    sbResponse.Append(game["ImagenDiscord"]);
                    sbResponse.Append(Environment.NewLine);
                }
                else
                {
                    sbResponse.Append("ImagenDiscord: (No capturaste la vez anterior, si no actualizarás este campo eliminalo de los datos.)");
                    sbResponse.Append(Environment.NewLine);
                }

                if (game["ImagenJuego", TiposDevolver.Boleano])
                {
                    sbResponse.Append("ImagenJuego: ");
                    sbResponse.Append(game["ImagenJuego"]);
                    sbResponse.Append(Environment.NewLine);
                }
                else
                {
                    sbResponse.Append("ImagenJuego: (No capturaste la vez anterior, si no actualizarás este campo eliminalo de los datos.)");
                    sbResponse.Append(Environment.NewLine);
                }

                if (game["Peso", TiposDevolver.Boleano])
                {
                    sbResponse.Append("Peso: ");
                    sbResponse.Append(game["Peso"]);
                    sbResponse.Append(Environment.NewLine);
                }
                else
                {
                    sbResponse.Append("Peso: (No capturaste la vez anterior, si no actualizarás este campo eliminalo de los datos.)");
                    sbResponse.Append(Environment.NewLine);
                }

                if (game["Formato", TiposDevolver.Boleano])
                {
                    sbResponse.Append("Formato: ");
                    sbResponse.Append(game["Formato"]);
                    sbResponse.Append(Environment.NewLine);
                }
                else
                {
                    sbResponse.Append("Formato: (No capturaste la vez anterior, si no actualizarás este campo eliminalo de los datos.)");
                    sbResponse.Append(Environment.NewLine);
                }

                sbResponse.Append("Links:");
                sbResponse.Append(Environment.NewLine);
                foreach (var juego in game["Links"].toArray())
                {
                    sbResponse.Append(juego);
                    sbResponse.Append(Environment.NewLine);
                }
                sbResponse.Append(Environment.NewLine);

                if(game["UpdateIndex", TiposDevolver.Boleano] && searchUpdate("", ref update, game["UpdateIndex", TiposDevolver.Entero]))
                {
                    if (update["Version", TiposDevolver.Boleano])
                    {
                        sbResponse.Append("Version: ");
                        sbResponse.Append(update["Version"]);
                        sbResponse.Append(Environment.NewLine);
                    }
                    else
                    {
                        sbResponse.Append("Version: (No capturaste la vez anterior, si no actualizarás este campo eliminalo de los datos.)");
                        sbResponse.Append(Environment.NewLine);
                    }

                    if (update["Peso", TiposDevolver.Boleano])
                    {
                        sbResponse.Append("Peso: ");
                        sbResponse.Append(update["Peso"]);
                        sbResponse.Append(Environment.NewLine);
                    }
                    else
                    {
                        sbResponse.Append("Peso: (No capturaste la vez anterior, si no actualizarás este campo eliminalo de los datos.)");
                        sbResponse.Append(Environment.NewLine);
                    }

                    if (update["Formato", TiposDevolver.Boleano])
                    {
                        sbResponse.Append("Formato: ");
                        sbResponse.Append(update["Formato"]);
                        sbResponse.Append(Environment.NewLine);
                    }
                    else
                    {
                        sbResponse.Append("Formato: (No capturaste la vez anterior, si no actualizarás este campo eliminalo de los datos.)");
                        sbResponse.Append(Environment.NewLine);
                    }

                    sbResponse.Append("Links:");
                    sbResponse.Append(Environment.NewLine);
                    foreach (var updateLink in update["Links"].toArray())
                    {
                        sbResponse.Append(updateLink);
                        sbResponse.Append(Environment.NewLine);
                    }
                    sbResponse.Append(Environment.NewLine);
                }

                if (game["DlcIndex", TiposDevolver.Boleano] && searchDlc("", ref dlc, game["DlcIndex", TiposDevolver.Entero]))
                {
                    if (dlc["Peso", TiposDevolver.Boleano])
                    {
                        sbResponse.Append("Peso: ");
                        sbResponse.Append(dlc["Peso"]);
                        sbResponse.Append(Environment.NewLine);
                    }
                    else
                    {
                        sbResponse.Append("Peso: (No capturaste la vez anterior, si no actualizarás este campo eliminalo de los datos.)");
                        sbResponse.Append(Environment.NewLine);
                    }

                    if (dlc["Formato", TiposDevolver.Boleano])
                    {
                        sbResponse.Append("Formato: ");
                        sbResponse.Append(dlc["Formato"]);
                        sbResponse.Append(Environment.NewLine);
                    }
                    else
                    {
                        sbResponse.Append("Formato: (No capturaste la vez anterior, si no actualizarás este campo eliminalo de los datos.)");
                        sbResponse.Append(Environment.NewLine);
                    }

                    sbResponse.Append("Links:");
                    sbResponse.Append(Environment.NewLine);
                    foreach (var dlcLink in dlc["Links"].toArray())
                    {
                        sbResponse.Append(dlcLink);
                        sbResponse.Append(Environment.NewLine);
                    }
                    sbResponse.Append(Environment.NewLine);
                }
            }

            return sbResponse.ToString();
        }

        /// <summary>
        /// Genera el builder para el juego recibido como parametro
        /// </summary>
        /// <param name="game">Corresponde a los datos del juego</param>
        /// <param name="builderGame">Correspone al builder donde se harán las configuraciones</param>
        /// <returns>Regresa verdadero si todo se ejecuta correctamente.</returns>
        public static bool generaBuilderGame(stdClassCSharp game,ref EmbedBuilder builderGame, ref string jdwonloader, ref int totalLinks)
        {
            bool respuesta = false;
            try
            {
                StringBuilder sbDatos = new StringBuilder();
                int parte = 0;
                string linkTemporal = "";
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

                foreach (string link in game["Links"].toArray())
                {
                    if (sbDatos.Length > 0)
                        sbDatos.Append(Environment.NewLine);
                    if (sbDatos.Length + link.ToString().Length > 1024)
                    {
                        parte++;
                        builderGame.AddField($"Links Del Juego parte {parte}", sbDatos.ToString());
                        sbDatos.Clear();
                    }

                    sbDatos.Append(link);
                    linkTemporal = link.Substring(0, link.Length - 1);
                    linkTemporal = linkTemporal.Substring(linkTemporal.IndexOf('(') + 1);
                    jdwonloader = $"{jdwonloader},{linkTemporal}";
                    totalLinks++;
                }

                if (parte > 0)
                {
                    parte++;
                    builderGame.AddField($"Links Del Juego parte {parte}", sbDatos.ToString());
                }
                else
                    builderGame.AddField($"Links Del Juego", sbDatos.ToString());

                sbDatos.Clear();
                parte = 0;

                if (game["UpdateIndex", TiposDevolver.Boleano])
                {
                    stdClassCSharp update = new stdClassCSharp();

                    if (searchUpdate(game["Titulo"], ref update, game["UpdateIndex",TiposDevolver.Entero])
                        && update.toArray().Length > 0)
                    {
                        game["Update"] = update;
                        stdClassCSharp links = game["Update"].Links;
                        foreach (string link in links.toArray())
                        {
                            if (sbDatos.Length > 0)
                                sbDatos.Append(Environment.NewLine);
                            if(sbDatos.Length + link.ToString().Length > 1024)
                            {
                                parte++;
                                builderGame.AddField($"Links Update {game["Update"].Version} {parte}", sbDatos.ToString());
                                sbDatos.Clear();
                            }

                            sbDatos.Append(link);
                            linkTemporal = link.Substring(0, link.Length - 1);
                            linkTemporal = linkTemporal.Substring(linkTemporal.IndexOf('(') + 1);
                            jdwonloader = $"{jdwonloader},{linkTemporal}";
                            totalLinks++;
                        }

                        if (parte > 0)
                        {
                            parte++;
                            builderGame.AddField($"Links Update {game["Update"].Version} {parte}", sbDatos.ToString());
                        }
                        else
                            builderGame.AddField($"Links Update {game["Update"].Version}", sbDatos.ToString());

                        sbDatos.Clear();
                        parte = 0;
                    }
                }

                if (game["DlcIndex", TiposDevolver.Boleano])
                {
                    stdClassCSharp dlc = new stdClassCSharp();

                    if (searchDlc(game["Titulo"], ref dlc, game["DlcIndex", TiposDevolver.Entero])
                        && dlc.toArray().Length > 0)
                    {
                        game["Dlc"] = dlc;
                        stdClassCSharp links = game["Dlc"].Links;
                        foreach (string link in links.toArray())
                        {
                            if (sbDatos.Length > 0)
                                sbDatos.Append(Environment.NewLine);
                            if (sbDatos.Length + link.ToString().Length > 1024)
                            {
                                parte++;
                                builderGame.AddField($"DLC's parte {parte}", sbDatos.ToString());
                                sbDatos.Clear();
                            }

                            sbDatos.Append(link);
                            linkTemporal = link.Substring(0, link.Length - 1);
                            linkTemporal = linkTemporal.Substring(linkTemporal.IndexOf('(') + 1);
                            jdwonloader = $"{jdwonloader},{linkTemporal}";
                            totalLinks++;
                        }

                        if (parte > 0)
                        {
                            parte++;
                            builderGame.AddField($"DLC's parte {parte}", sbDatos.ToString());
                        }
                        else
                            builderGame.AddField("DLC's", sbDatos.ToString());

                        sbDatos.Clear();
                        parte = 0;
                    }
                }
                jdwonloader = $"[Add JDownloader]({jdwonloader.Substring(1)}) <- Click Derecho - Copiar enlace";
                respuesta = true;
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
        public static bool generaBuilderUpdate(stdClassCSharp update,ref EmbedBuilder builderUpdate, ref string jdwonloader, ref int totalLinks)
        {
            bool respuesta = false;
            try
            {
                StringBuilder sbDatos = new StringBuilder();
                string linkTemporal = "";
                int parte = 0;
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
                    if (sbDatos.Length + link.ToString().Length > 1024)
                    {
                        parte++;
                        builderUpdate.AddField($"Links parte {parte}", sbDatos.ToString());
                        sbDatos.Clear();
                    }


                    sbDatos.Append(link);
                    linkTemporal = link.Substring(0, link.Length - 1);
                    linkTemporal = linkTemporal.Substring(linkTemporal.IndexOf('(') + 1);
                    jdwonloader = $"{jdwonloader},{linkTemporal}";
                    totalLinks++;
                }

                if (parte > 0)
                {
                    parte++;
                    builderUpdate.AddField($"Links parte {parte}", sbDatos.ToString());
                }
                else
                    builderUpdate.AddField($"Links", sbDatos.ToString());

                sbDatos.Clear();

                parte = 0;
                jdwonloader = $"[Add JDownloader]({jdwonloader.Substring(1)}) <- Click Derecho - Copiar enlace";

                respuesta = true;
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
        public static bool generaBuilderDlc(stdClassCSharp dlc, ref EmbedBuilder builderDlc, ref string jdwonloader, ref int totalLinks)
        {
            bool respuesta = false;
            try
            {
                StringBuilder sbDatos = new StringBuilder();
                int parte = 0;
                string linkTemporal = "";
                sbDatos.Append("**");
                sbDatos.Append(dlc["Titulo"]);
                sbDatos.Append("**");

                builderDlc.WithTitle(sbDatos.ToString());

                sbDatos.Clear();
                
                builderDlc.WithColor(0xFFFFFF);
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
                    if (sbDatos.Length + link.ToString().Length > 1024)
                    {
                        parte++;
                        builderDlc.AddField($"Links parte {parte}", sbDatos.ToString());
                        sbDatos.Clear();
                    }
                    sbDatos.Append(link);
                    linkTemporal = link.Substring(0, link.Length - 1);
                    linkTemporal = linkTemporal.Substring(linkTemporal.IndexOf('(') + 1);
                    jdwonloader = $"{jdwonloader},{linkTemporal}";
                    totalLinks++;
                }

                if(parte > 0)
                {
                    parte++;
                    builderDlc.AddField($"Links parte {parte}", sbDatos.ToString());
                }
                else
                    builderDlc.AddField("Links", sbDatos.ToString());

                sbDatos.Clear();
                parte = 0;

                jdwonloader = $"[Add JDownloader]({jdwonloader.Substring(1)}) <- Click Derecho - Copiar enlace";
                respuesta = true;
            }
            catch
            {
                respuesta = false;
            }
            return respuesta;
        }

        /// <summary>
        /// Guarda los datos de juego desde un mensaje mp
        /// </summary>
        /// <param name="index">Determina el indice del juego a modificar, si es juego nuevo obtendrá -1</param>
        public static string guardarJuego(string messageGame, ref string anuncialo, int index = -1)
        {
            string respuesta = "Tu juego se ha agregado correctamente a la lista.";
            stdClassCSharp gamesStd = stdClassCSharp.readJsonFile("games.json");
            stdClassCSharp updatesStd = stdClassCSharp.readJsonFile("updates.json");
            stdClassCSharp dlcStd = stdClassCSharp.readJsonFile("dlcs.json");
            stdClassCSharp game = new stdClassCSharp();
            stdClassCSharp update = new stdClassCSharp();
            stdClassCSharp dlc = new stdClassCSharp();
            stdClassCSharp gameActual = new stdClassCSharp();
            int upateIndex = -1;
            int dlcIndex = -1;
            generaDatosJuego(messageGame, ref game, ref update, ref dlc);

            if (index >= 0)
            {
                respuesta = "Tu juego se ha editado correctamente";
                searchGame("", ref gameActual, index);
                if (gameActual["DlcIndex", TiposDevolver.Boleano])
                {
                    dlcIndex = gameActual["DlcIndex", TiposDevolver.Entero];
                }
                else if (gameActual["DlcIndexAnt", TiposDevolver.Boleano])
                {
                    dlcIndex = gameActual["DlcIndexAnt", TiposDevolver.Entero];
                }

                if (gameActual["UpdateIndex", TiposDevolver.Boleano])
                {
                    upateIndex = gameActual["UpdateIndex", TiposDevolver.Entero];
                }
                else if (gameActual["UpdateIndexAnt", TiposDevolver.Boleano])
                {
                    upateIndex = gameActual["UpdateIndex", TiposDevolver.Entero];
                }
            }

            gameActual = game;

            if (game["Titulo", TiposDevolver.Boleano] && game["Links", TiposDevolver.Boleano])
            {
                if (dlc["Links", TiposDevolver.Boleano])
                {
                    dlc["Titulo"] = game["Titulo"];
                    dlc["ImagenJuego"] = game["ImagenJuego"];
                    dlc["ImagenDiscord"] = game["ImagenDiscord"];
                    dlc["UploadBy"] = game["UploadBy"];

                    if (dlcIndex >= 0)
                    {
                        dlcStd[dlcIndex] = dlc;
                        gameActual["DlcIndex"] = dlcIndex;
                        if (gameActual["DlcIndexAnt", TiposDevolver.Boleano])
                        {
                            gameActual.Remove("DlcIndexAnt");
                        }
                    }
                    else
                    {
                        dlcStd.Add(dlc);
                        game["DlcIndex"] = dlcStd.toArray().Length - 1;
                    }
                }
                else if (dlcIndex >= 0)
                {
                    gameActual["DlcIndexAnt"] = dlcIndex;
                }
                if (update["Version", TiposDevolver.Boleano] && update["Links", TiposDevolver.Boleano])
                {
                    update["Titulo"] = game["Titulo"];
                    update["ImagenJuego"] = game["ImagenJuego"];
                    update["ImagenDiscord"] = game["ImagenDiscord"];
                    update["UploadBy"] = game["UploadBy"];

                    if (upateIndex >= 0)
                    {
                        updatesStd[upateIndex] = update;
                        gameActual["UpdateIndex"] = dlcIndex;
                        if (gameActual["UpdateIndexAnt", TiposDevolver.Boleano])
                        {
                            gameActual.Remove("UpdateIndexAnt");
                        }
                    }
                    else
                    {
                        updatesStd.Add(update);
                        game["UpdateIndex"] = updatesStd.toArray().Length - 1;
                    }
                }
                else if (update["Links", TiposDevolver.Boleano] && !dlc["Links", TiposDevolver.Boleano])
                {
                    if (upateIndex >= 0)
                    {
                        gameActual["UpdateIndexAnt"] = upateIndex;
                    }
                    dlc["Links"] = update["Links"];
                    dlc["Peso"] = update["Peso"];
                    dlc["Formato"] = update["Formato"];
                    dlc["Titulo"] = game["Titulo"];
                    dlc["ImagenJuego"] = game["ImagenJuego"];
                    dlc["ImagenDiscord"] = game["ImagenDiscord"];
                    dlc["UploadBy"] = game["UploadBy"];

                    if (dlcIndex >= 0)
                    {
                        dlcStd[dlcIndex] = dlc;
                        gameActual["DlcIndex"] = dlcIndex;
                        if (gameActual["DlcIndexAnt", TiposDevolver.Boleano])
                        {
                            gameActual.Remove("DlcIndexAnt");
                        }
                    }
                    else
                    {
                        dlcStd.Add(dlc);
                        game["DlcIndex"] = dlcStd.toArray().Length - 1;
                    }
                }
                else if (upateIndex >= 0)
                {
                    gameActual["UpdateIndexAnt"] = upateIndex;
                }
                if (index < 0)
                    gamesStd.Add(game);
                else
                    gamesStd[index] = game;
                gamesStd.writeJsonFile("games.json");
                updatesStd.writeJsonFile("updates.json");
                dlcStd.writeJsonFile("dlcs.json");
                if(string.IsNullOrWhiteSpace(anuncialo) && index == -1)
                    anuncialo = $"El juego {game["Titulo"]} ha sido agregado a mi lista de juegos solicitalo con el comando !game [juegoabuscar]";
            }
            else
            {
                respuesta = "No capturaste titulo o links del juego.";
            }

            return respuesta;
        }

        private static void generaDatosJuego(string messageGame, ref stdClassCSharp game, ref stdClassCSharp update, ref stdClassCSharp dlc)
        {
            string[] datosDelJuego = messageGame.Split('\n');//new String[] { Environment.NewLine }, StringSplitOptions.None);
            string propiedad = "", valor = "";
            int tipoDatoJuego = 0;
            for(int i = 0; i < datosDelJuego.Length; i++)
            {
                propiedad = datosDelJuego[i].Substring(0, datosDelJuego[i].IndexOf(':'));
                valor = datosDelJuego[i].Substring(datosDelJuego[i].IndexOf(':') + 1);

                if(propiedad.ToLower().Equals("uploader"))
                {
                    if(tipoDatoJuego == 0)
                        game["UploadBy"] = valor.Trim();
                }
                else if(propiedad.ToLower().Equals("updateversion"))
                {
                    if (tipoDatoJuego == 0)
                        update["Version"] = valor.Trim();
                }
                else if(propiedad.ToLower().Equals("links"))
                {
                    if (tipoDatoJuego == 0)
                    {
                        game[propiedad] = new stdClassCSharp(true);
                        i++;
                        while(i < datosDelJuego.Length && !string.IsNullOrEmpty(datosDelJuego[i]))
                        {
                            (game[propiedad] as stdClassCSharp).Add(datosDelJuego[i]);
                            i++;
                        }
                        tipoDatoJuego++;
                    }
                    else if (tipoDatoJuego == 1)
                    {
                        update[propiedad] = new stdClassCSharp(true);
                        i++;
                        while (i < datosDelJuego.Length && !string.IsNullOrEmpty(datosDelJuego[i]))
                        {
                            (update[propiedad] as stdClassCSharp).Add(datosDelJuego[i]);
                            i++;
                        }
                        tipoDatoJuego++;
                    }
                    else if (tipoDatoJuego == 2)
                    {
                        dlc[propiedad] = new stdClassCSharp(true);
                        i++;
                        while (i < datosDelJuego.Length && !string.IsNullOrEmpty(datosDelJuego[i]))
                        {
                            (dlc[propiedad] as stdClassCSharp).Add(datosDelJuego[i]);
                            i++;
                        }
                        tipoDatoJuego++;
                    }
                }
                else
                {
                    if(tipoDatoJuego == 0)
                        game[propiedad] = valor.Trim();
                    else if(tipoDatoJuego == 1)
                        update[propiedad] = valor.Trim();
                    else if (tipoDatoJuego == 2)
                        dlc[propiedad] = valor.Trim();
                }
            }
        }
    }
}
