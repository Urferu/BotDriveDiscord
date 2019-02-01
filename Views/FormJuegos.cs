using System;
using System.Windows.Forms;
using DriveBot.Resources.Utils;

namespace DriveBot.Views
{
    public partial class FormJuegos : Form
    {
        stdClassCSharp games;
        public FormJuegos()
        {
            InitializeComponent();
            games = stdClassCSharp.readJsonFile("games.json");
        }

        private void FormJuegos_Load(object sender, System.EventArgs e)
        {
            llenarGrid();
        }

        private void btnNuevoJuego_Click(object sender, System.EventArgs e)
        {
            FormDatosDelJuego datosDelJuego = new FormDatosDelJuego();
            datosDelJuego.ShowDialog();
            games = stdClassCSharp.readJsonFile("games.json");
            if (txtFiltro.Text == "")
                llenarGrid();
            else
                txtFiltro.Text = "";
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if(MessageBox.Show("¿Esta Seguro que desea eliminar el juego seleccionado?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    == DialogResult.Yes)
                {
                    games = stdClassCSharp.readJsonFile("games.json");
                    if(games["UpdateIndex", TiposDevolver.Boleano])
                    {
                        stdClassCSharp updates = stdClassCSharp.readJsonFile("updates.json");
                        updates.Remove(games["UpdateIndex", TiposDevolver.Entero]);
                        updates.writeJsonFile("updates.json");
                    }

                    if (games["DlcIndex", TiposDevolver.Boleano])
                    {
                        stdClassCSharp dlcs = stdClassCSharp.readJsonFile("dlcs.json");
                        dlcs.Remove(games["DlcIndex", TiposDevolver.Entero]);
                        dlcs.writeJsonFile("dlcs.json");
                    }
                    games.Remove(Convert.ToInt32(dataGridView1.CurrentRow.Cells["colIndex"].Value));
                    games.writeJsonFile("games.json");
                    if (txtFiltro.Text == "")
                        llenarGrid();
                    else
                        txtFiltro.Text = "";
                }
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                FormDatosDelJuego datosDelJuego = new FormDatosDelJuego(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["colIndex"].Value));
                datosDelJuego.ShowDialog();
                games = stdClassCSharp.readJsonFile("games.json");
                if (txtFiltro.Text == "")
                    llenarGrid();
                else
                    txtFiltro.Text = "";
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text.Trim().Length > 3)
            {
                filtrarGrid();
            }
            else if(txtFiltro.Text == "")
            {
                llenarGrid();
            }
        }

        private void llenarGrid()
        {
            dataGridView1.Rows.Clear();
            foreach (stdClassCSharp game in games.toArray())
            {
                dataGridView1.Rows.Add(game["Titulo"], dataGridView1.Rows.Count);
            }
        }

        private void filtrarGrid()
        {
            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if(!dataGridView1.Rows[i].Cells["colJuego"].Value.ToString().ToLower().Contains(txtFiltro.Text.Trim().ToLower()))
                {
                    dataGridView1.Rows.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
