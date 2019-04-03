using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Drawing;
using System.Windows.Forms;
using Discord;
using DriveBot.Resources.Utils;
using Discord.Commands;

namespace DriveBot.Views
{
    public partial class FormServer : Form
    {
        stdClassCSharp token;
        public FormServer()
        {
            InitializeComponent();
            token = stdClassCSharp.readJsonFile("token.json");
            txtTocken.Text = token["token"];
            //validaEmbeeds();
        }

        private void validaEmbeeds()
        {
            var builders = new List<EmbedBuilder>();
            var builder = new EmbedBuilder();
            int fields = 0;
            string listaLetraAnterior = "";
            try
            {
                builder.WithTitle("Estos son los juegos que tengo disponibles:");
                builder.WithColor(new Color(0xAF3A));
                foreach (string listaLetra in Utils.getListGames("T"))
                {
                    if (fields >= 24)
                    {
                        builders.Add(builder);

                        builder = new EmbedBuilder();
                        builder.WithColor(new Color(0xAF3A));
                        fields = 0;
                    }
                    if (listaLetra.Length > 0 && listaLetra.Substring(0, 1) == listaLetraAnterior)
                        builder.AddField($"{listaLetra.Substring(0, 1)} continuación", listaLetra);
                    else
                        builder.AddField(listaLetra.Substring(0, 1), listaLetra);

                    fields++;
                    listaLetraAnterior = listaLetra.Substring(0, 1);
                }
                if (fields >= 24)
                {
                    builders.Add(builder);

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

                builders.Add(builder);
            }
            catch
            {
                MessageBox.Show(listaLetraAnterior);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            token["token"] = txtTocken.Text;
            btnConectar.Enabled = false;
            bwBot.RunWorkerAsync();
            token.writeJsonFile("token.json");
        }

        private void bwBot_DoWork(object sender, DoWorkEventArgs e)
        {
            new Program().iniciaServer(token["token"]);
        }

        private void btnAgregarJuego_Click(object sender, EventArgs e)
        {
            FormJuegos formJuegos = new FormJuegos();
            formJuegos.ShowDialog();
        }
    }
}
