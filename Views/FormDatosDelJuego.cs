using System.Windows.Forms;
using System;
using System.Text;
using DriveBot.Resources.Utils;

namespace DriveBot.Views
{
    public partial class FormDatosDelJuego : Form
    {
        stdClassCSharp game;
        stdClassCSharp update;
        stdClassCSharp dlc;
        int indexGame;
        public FormDatosDelJuego(int indiceJuego = -1)
        {
            InitializeComponent();
            game = new stdClassCSharp();
            update = new stdClassCSharp();
            dlc = new stdClassCSharp();
            indexGame = indiceJuego;
            if(indiceJuego >= 0)
                Utils.searchGame("", ref game, indiceJuego);
            if(game["UpdateIndex", TiposDevolver.Boleano])
            {
                Utils.searchUpdate("", ref update, game["UpdateIndex", TiposDevolver.Entero]);
            }

            if (game["DlcIndex", TiposDevolver.Boleano])
            {
                Utils.searchDlc("", ref dlc, game["DlcIndex", TiposDevolver.Entero]);
            }
        }

        private void FormDatosDelJuego_Load(object sender, System.EventArgs e)
        {
            StringBuilder sbDatos = new StringBuilder();
            

            if (game.toArray().Length > 0)
            {
                txtTitulo.Text = game["Titulo"];

                if (game["Descripcion", TiposDevolver.Boleano])
                {
                    txtDescripcion.Text = game["Descripcion"];
                }

                if (game["UploadBy", TiposDevolver.Boleano])
                {
                    txtUploadBy.Text = game["UploadBy"];
                }

                if (game["Peso", TiposDevolver.Boleano])
                {
                    txtPeso.Text = game["Peso"];
                }

                if (game["Formato", TiposDevolver.Boleano])
                {
                    txtFormato.Text = game["Formato"];
                }

                if (game["Password", TiposDevolver.Boleano])
                {
                    txtPassword.Text = game["Password"];
                }

                txtPortada.Text = game["ImagenJuego"];
                txtUrlAvatar.Text = game["ImagenDiscord"];

                foreach(var link in game["Links"].toArray())
                {
                    if(sbDatos.Length > 0)
                        sbDatos.Append(Environment.NewLine);

                    sbDatos.Append(link);
                }

                txtGameLinks.Text = sbDatos.ToString();

                sbDatos.Clear();
            }

            if (update.toArray().Length > 0)
            {
                txtVersion.Text = update["Version"];

                if (update["Peso", TiposDevolver.Boleano])
                {
                    txtPesoUpdate.Text = update["Peso"];
                }

                if (update["Formato", TiposDevolver.Boleano])
                {
                    txtFormatoUpdate.Text = update["Formato"];
                }

                if (update["Password", TiposDevolver.Boleano])
                {
                    txtPasswordUpdate.Text = update["Password"];
                }

                foreach (var link in update["Links"].toArray())
                {
                    if (sbDatos.Length > 0)
                        sbDatos.Append(Environment.NewLine);

                    sbDatos.Append(link);
                }

                txtLinksUpdate.Text = sbDatos.ToString();

                sbDatos.Clear();
            }

            if (dlc.toArray().Length > 0)
            {
                if (dlc["Peso", TiposDevolver.Boleano])
                {
                    txtPesoDlc.Text = dlc["Peso"];
                }

                if (dlc["Formato", TiposDevolver.Boleano])
                {
                    txtformatoDlc.Text = dlc["Formato"];
                }

                if (dlc["Password", TiposDevolver.Boleano])
                {
                    txtPasswordDlc.Text = dlc["Password"];
                }

                foreach (var link in dlc["Links"].toArray())
                {
                    if (sbDatos.Length > 0)
                        sbDatos.Append(Environment.NewLine);

                    sbDatos.Append(link);
                }

                txtLinksDlc.Text = sbDatos.ToString();

                sbDatos.Clear();
            }

            
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text) || string.IsNullOrWhiteSpace(txtGameLinks.Text))
            {
                MessageBox.Show("Es necesario capturar almenos nombre y links del juego");
                return;
            }

            guardarJuego();

            this.Close();
        }

        private void guardarJuego()
        {
            stdClassCSharp gamesStd = stdClassCSharp.readJsonFile("games.json");
            stdClassCSharp updatesStd = stdClassCSharp.readJsonFile("updates.json");
            stdClassCSharp dlcStd = stdClassCSharp.readJsonFile("dlcs.json");

            game["Titulo"] = txtTitulo.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txtDescripcion.Text))
                game["Descripcion"] = txtDescripcion.Text;
            else if (game["Descripcion", TiposDevolver.Boleano])
                game.Remove("Descripcion");

            if (!string.IsNullOrWhiteSpace(txtPeso.Text))
                game["Peso"] = txtPeso.Text;
            else if (game["Peso", TiposDevolver.Boleano])
                game.Remove("Peso");

            if (!string.IsNullOrWhiteSpace(txtFormato.Text))
                game["Formato"] = txtFormato.Text;
            else if (game["Formato", TiposDevolver.Boleano])
                game.Remove("Formato");

            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                game["Password"] = txtPassword.Text;
            else if (game["Password", TiposDevolver.Boleano])
                game.Remove("Password");

            if (!string.IsNullOrWhiteSpace(txtUploadBy.Text))
                game["UploadBy"] = txtUploadBy.Text;
            else if (game["UploadBy", TiposDevolver.Boleano])
                game.Remove("UploadBy");

            if (!string.IsNullOrWhiteSpace(txtUrlAvatar.Text))
                game["ImagenDiscord"] = txtUrlAvatar.Text;
            else if (game["ImagenDiscord", TiposDevolver.Boleano])
                game.Remove("ImagenDiscord");

            if (!string.IsNullOrWhiteSpace(txtPortada.Text))
                game["ImagenJuego"] = txtPortada.Text;
            else if (game["ImagenJuego", TiposDevolver.Boleano])
                game.Remove("ImagenJuego");

            game["Links"] = new stdClassCSharp(true);

            foreach(string link in txtGameLinks.Text.Trim().Split(new String[] { Environment.NewLine }, StringSplitOptions.None))
            {
                game["Links"].Add(link);
            }

            if (!string.IsNullOrWhiteSpace(txtVersion.Text) && !string.IsNullOrWhiteSpace(txtLinksUpdate.Text))
            {
                update["Titulo"] = txtTitulo.Text.Trim();

                update["Version"] = txtVersion.Text.Trim();

                if (!string.IsNullOrWhiteSpace(txtPesoUpdate.Text))
                    update["Peso"] = txtPesoUpdate.Text;
                else if (update["Peso", TiposDevolver.Boleano])
                    update.Remove("Peso");

                if (!string.IsNullOrWhiteSpace(txtFormatoUpdate.Text))
                    update["Formato"] = txtFormatoUpdate.Text;
                else if (update["Formato", TiposDevolver.Boleano])
                    update.Remove("Formato");

                if (!string.IsNullOrWhiteSpace(txtPasswordUpdate.Text))
                    update["Password"] = txtPasswordUpdate.Text;
                else if (update["Password", TiposDevolver.Boleano])
                    update.Remove("Password");

                if (!string.IsNullOrWhiteSpace(txtUploadBy.Text))
                    update["UploadBy"] = txtUploadBy.Text;
                else if (update["UploadBy", TiposDevolver.Boleano])
                    update.Remove("UploadBy");

                if (!string.IsNullOrWhiteSpace(txtUrlAvatar.Text))
                    update["ImagenDiscord"] = txtUrlAvatar.Text;
                else if (update["ImagenDiscord", TiposDevolver.Boleano])
                    update.Remove("ImagenDiscord");

                if (!string.IsNullOrWhiteSpace(txtPortada.Text))
                    update["ImagenJuego"] = txtPortada.Text;
                else if (update["ImagenJuego", TiposDevolver.Boleano])
                    update.Remove("ImagenJuego");

                update["Links"] = new stdClassCSharp(true);

                foreach (string link in txtLinksUpdate.Text.Trim().Split(new String[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    update["Links"].Add(link);
                }
            }

            if (!string.IsNullOrWhiteSpace(txtLinksDlc.Text))
            {
                dlc["Titulo"] = txtTitulo.Text.Trim();

                if (!string.IsNullOrWhiteSpace(txtPesoDlc.Text))
                    dlc["Peso"] = txtPesoDlc.Text;
                else if (dlc["Peso", TiposDevolver.Boleano])
                    dlc.Remove("Peso");

                if (!string.IsNullOrWhiteSpace(txtformatoDlc.Text))
                    dlc["Formato"] = txtformatoDlc.Text;
                else if (dlc["Formato", TiposDevolver.Boleano])
                    dlc.Remove("Formato");

                if (!string.IsNullOrWhiteSpace(txtPasswordDlc.Text))
                    dlc["Password"] = txtPasswordDlc.Text;
                else if (dlc["Password", TiposDevolver.Boleano])
                    dlc.Remove("Password");

                if (!string.IsNullOrWhiteSpace(txtUploadBy.Text))
                    dlc["UploadBy"] = txtUploadBy.Text;
                else if (dlc["UploadBy", TiposDevolver.Boleano])
                    dlc.Remove("UploadBy");

                if (!string.IsNullOrWhiteSpace(txtUrlAvatar.Text))
                    dlc["ImagenDiscord"] = txtUrlAvatar.Text;
                else if (dlc["ImagenDiscord", TiposDevolver.Boleano])
                    dlc.Remove("ImagenDiscord");

                if (!string.IsNullOrWhiteSpace(txtPortada.Text))
                    dlc["ImagenJuego"] = txtPortada.Text;
                else if (dlc["ImagenJuego", TiposDevolver.Boleano])
                    dlc.Remove("ImagenJuego");

                dlc["Links"] = new stdClassCSharp(true);

                foreach (string link in txtLinksDlc.Text.Trim().Split(new String[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    dlc["Links"].Add(link);
                }
            }

            if (game["UpdateIndex", TiposDevolver.Boleano])
            {
                if (update.toArray().Length == 0)
                {
                    updatesStd.Remove(game["UpdateIndex", TiposDevolver.Entero]);
                    game.Remove("UpdateIndex");
                }
                else
                {
                    updatesStd[game["UpdateIndex", TiposDevolver.Entero]] = update;
                }
            }
            else if (update.toArray().Length > 0)
            {
                updatesStd.Add(update);
                game["UpdateIndex"] = updatesStd.toArray().Length - 1;
            }

            if (game["DlcIndex", TiposDevolver.Boleano])
            {
                if (dlc.toArray().Length == 0)
                {
                    dlcStd.Remove(game["DlcIndex", TiposDevolver.Entero]);
                    game.Remove("DlcIndex");
                }
                else
                {
                    dlcStd[game["DlcIndex", TiposDevolver.Entero]] = dlc;
                }
            }
            else if (dlc.toArray().Length > 0)
            {
                dlcStd.Add(dlc);
                game["DlcIndex"] = dlcStd.toArray().Length - 1;
            }

            if (indexGame == -1)
            {
                gamesStd.Add(game);
            }
            else
            {
                gamesStd[indexGame] = game;
            }

            gamesStd.writeJsonFile("games.json");
            updatesStd.writeJsonFile("updates.json");
            dlcStd.writeJsonFile("dlcs.json");
        }
    }
}
