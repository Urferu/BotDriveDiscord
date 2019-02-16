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
        string tocken;
        public FormServer()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tocken = txtTocken.Text;
            btnConectar.Enabled = false;
            bwBot.RunWorkerAsync();
        }

        private void bwBot_DoWork(object sender, DoWorkEventArgs e)
        {
            new Program().iniciaServer(tocken);
        }

        private void btnAgregarJuego_Click(object sender, EventArgs e)
        {
            FormJuegos formJuegos = new FormJuegos();
            formJuegos.ShowDialog();
        }
    }
}
