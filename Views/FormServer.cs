using System;
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
