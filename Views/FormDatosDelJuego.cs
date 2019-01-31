using System.Windows.Forms;
using System.Text;
using DriveBot.Resources.Utils;

namespace DriveBot.Views
{
    public partial class FormDatosDelJuego : Form
    {
        stdClassCSharp game;
        stdClassCSharp update;
        stdClassCSharp dlc;
        public FormDatosDelJuego(int indiceJuego = -1)
        {
            InitializeComponent();
            game = new stdClassCSharp();
            update = new stdClassCSharp();
            dlc = new stdClassCSharp();
            Utils.searchGame("", ref game, indiceJuego);
            if(game["UpdateIndex", TiposDevolver.Boleano])
            {
                Utils.searchUpdate("", ref update, game["UpdateIndex", TiposDevolver.Entero]);
            }

            if (game["DlcIndex", TiposDevolver.Boleano])
            {
                Utils.searchUpdate("", ref dlc, game["DlcIndex", TiposDevolver.Entero]);
            }
        }

        private void FormDatosDelJuego_Load(object sender, System.EventArgs e)
        {
            StringBuilder sbDatos = new StringBuilder();
            if(game.toArray().Length > 0)
            {
                txtTitulo.Text = game["Titulo"];
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
                    if(sbDatos.Length == 0)
                        sbDatos.Append("\n");

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
                    if (sbDatos.Length == 0)
                        sbDatos.Append("\n");

                    sbDatos.Append(link);
                }

                txtLinksUpdate.Text = sbDatos.ToString();

                sbDatos.Clear();
            }

            if (dlc.toArray().Length > 0)
            {
                if (dlc["Peso", TiposDevolver.Boleano])
                {
                    txtPesoUpdate.Text = dlc["Peso"];
                }

                if (dlc["Formato", TiposDevolver.Boleano])
                {
                    txtFormatoUpdate.Text = dlc["Formato"];
                }

                if (dlc["Password", TiposDevolver.Boleano])
                {
                    txtPasswordUpdate.Text = dlc["Password"];
                }

                foreach (var link in dlc["Links"].toArray())
                {
                    if (sbDatos.Length == 0)
                        sbDatos.Append("\n");

                    sbDatos.Append(link);
                }

                txtLinksDlc.Text = sbDatos.ToString();

                sbDatos.Clear();
            }
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtTitulo.Text) || string.IsNullOrWhiteSpace(txtGameLinks.Text))
            {
                MessageBox.Show("Es necesario capturar almenos nombre y links del juego");
                return;
            }
        }
    }
}
