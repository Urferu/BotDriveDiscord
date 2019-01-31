using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriveBot.Views
{
    public partial class FormServer : Form
    {
        public FormServer()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnConectar.Enabled = false;
            bwBot.RunWorkerAsync();
        }

        private void bwBot_DoWork(object sender, DoWorkEventArgs e)
        {
            new Program().iniciaServer(txtTocken.Text);
        }
    }
}
